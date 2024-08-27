using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.DeliveryStage;
using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Application.ViewModels.InspectionRequest;
using Application.ViewModels.PurchasingPlan;
using Application.ViewModels.WarehouseForm;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Inspection;
using Domain.Enums;
using Domain.Enums.DeliveryStage;
using System.Net;

namespace Application.Services
{
    public class DeliveryStageService : IDeliveryStageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPurchasingOrderService _purchasingOrderService;
        private readonly IClaimsService _claimsService;

        public DeliveryStageService(IUnitOfWork unitOfWork, IMapper mapper, IPurchasingOrderService purchasingOrderService, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _purchasingOrderService = purchasingOrderService;
            _claimsService = claimsService;
        }

        public async Task<DeliveryStageVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(id);
            if (item == null)
                throw new APIException(HttpStatusCode.NotFound,
                                       nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<DeliveryStageVM>(item);
            return result;
        }
        public async Task<DeliveryStageWithInspectionRequestVM> GetInspectedDeliveryStageByIdAsync(int id)
        {
            var item = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(id);
            if (item == null)
                throw new APIException(HttpStatusCode.NotFound,
                                       nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);


            if (item.DeliveryStatus != DeliveryStageStatusEnum.Inspected 
                && item.DeliveryStatus != DeliveryStageStatusEnum.TempWarehouseExported
                && item.DeliveryStatus != DeliveryStageStatusEnum.MainWarehouseImported)
                throw new APIException(HttpStatusCode.NotFound,
                                       nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Delivery stage is not inspected");

            var result = _mapper.Map<DeliveryStageWithInspectionRequestVM>(item);
            result.PurchaseMaterials.RemoveAll(x => x.TotalQuantity <= 0);

            var inspectionRequestList = await _unitOfWork.InspectionRequestRepo.GetAllByDeliveryStageIdAsync(id);
            var approvedInspectionRequest = inspectionRequestList.FirstOrDefault(x => x.ApproveStatus == ApproveEnum.Approved);
            var exportTempWarehouseForm = await _unitOfWork.WarehouseFormRepo.GetExportFormByDeliveryStageIdWithMaterialsAsync(id);
            var importMainWarehouseForm = await _unitOfWork.WarehouseFormRepo.GetImportMainFormByDeliveryStageIdWithMaterialsAsync(id);

            var inspectionRequestVM = _mapper.Map<InspectionRequestVM>(approvedInspectionRequest);
            var exportTempWarehouseFormVM = _mapper.Map<WarehouseFormVM>(exportTempWarehouseForm);
            var importMainWarehouseFormVM = _mapper.Map<WarehouseFormVM>(importMainWarehouseForm);


            result.InspectionRequest = inspectionRequestVM;
            result.ImportMainWarehouseForm = importMainWarehouseFormVM;
            result.ExportTempWarehouseForm = exportTempWarehouseFormVM;
            return result; 
        }

        public async Task<List<DeliveryStageVM>> GetAllAsync()
        {
            var items = await _unitOfWork.DeliveryStageRepo.GetAllWithDetailAsync();
            var result = _mapper.Map<List<DeliveryStageVM>>(items);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.DeliveryStageRepo.GetByIdAsync(id);

            _unitOfWork.DeliveryStageRepo.SoftRemove(itemToDelete);

            if (itemToDelete == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        // Change delivery stage status with checking the purchase material status
        public async Task ChangeDeliveryStageStatusAsync(int id, DeliveryStageStatusEnum status)
        {
            // Find existing delivery stage
            var existingItem = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(id);
            if (existingItem == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var isDeliveringStatus = status == DeliveryStageStatusEnum.Approved
                || status == DeliveryStageStatusEnum.Delivering
                || status == DeliveryStageStatusEnum.Delivered;

            if (isDeliveringStatus)
            {
                // Also update purchase material status to the same status
                existingItem.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive).ToList().ForEach(x => x.WarehouseStatus = status);


                // check if delivery date is between yesterday and tomorrow
                if (existingItem != null && existingItem.DeliveryDate != null)
                {

                    if (existingItem.DeliveryStatus == DeliveryStageStatusEnum.Approved
                        && (existingItem.DeliveryDate.Value.Date > DateTime.Today))
                    {
                        throw new APIException(HttpStatusCode.BadRequest,
                            nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Cannot perform delivering for this delivery stage now");
                    }
                }
            }
            else
            {
                var isPurchaseMaterialSameStatus = existingItem.PurchaseMaterials.All(x => (int)x.WarehouseStatus == (int)status);
                if (!isPurchaseMaterialSameStatus)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Purchase materials must have the same status as the assigning status");
                }
            }

            existingItem.DeliveryStatus = status;
            _unitOfWork.DeliveryStageRepo.Update(existingItem);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task TryChangeDeliveryStageStatusAsync(int id, DeliveryStageStatusEnum status)
        {
            // Find existing delivery stage
            var existingItem = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(id);
            if (existingItem == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var isDeliveringStatus = status == DeliveryStageStatusEnum.Approved
                || status == DeliveryStageStatusEnum.Delivering
                || status == DeliveryStageStatusEnum.Delivered;

            if (isDeliveringStatus)
            {
                // Also update purchase material status to the same status
                existingItem.PurchaseMaterials.ToList().ForEach(x => x.WarehouseStatus = status);
            }
            else
            {
                var isPurchaseMaterialSameStatus = existingItem.PurchaseMaterials.All(x => (int)x.WarehouseStatus == (int)status);
                if (!isPurchaseMaterialSameStatus)
                {
                    return;
                }
            }


            existingItem.DeliveryStatus = status;
            _unitOfWork.DeliveryStageRepo.Update(existingItem);
        }
                
        public async Task UpdateAsync(DeliveryStageUpdateVM updateItem)
        {
            var existingItem = await _unitOfWork.DeliveryStageRepo.GetByIdAsync(updateItem.Id);
            if (existingItem == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            _mapper.Map(updateItem, existingItem);

            _unitOfWork.DeliveryStageRepo.Update(existingItem);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task AddDeliveryStages(List<DeliveryStageAddVM> addingDeliveryStages, int purchaseOrderId)
        {
            var existingPurchaseOrder = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(purchaseOrderId);

            if (existingPurchaseOrder == null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - This purchasing order does not exist");
            }

            if (existingPurchaseOrder.ManagerApproveStatus != ApproveEnum.Approved || existingPurchaseOrder.SupplierApproveStatus != ApproveEnum.Approved)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.REQUEST_NOT_APPROVED), ExceptionMessage.REQUEST_NOT_APPROVED);
            }
                        
            // For each delivery stage, calculate the total price of each material for that stage
            addingDeliveryStages.ForEach(ds =>
                ds.PurchaseMaterials.ForEach(pm =>
                    pm.TotalPrice = pm.TotalQuantity * pm.PackagePrice));

            // For each delivery stage, calculate the total price of that stage by the previous total price
            foreach (var item in addingDeliveryStages) item.TotalPrice = item.PurchaseMaterials.Sum(x => x.TotalPrice);

            // Count the number of material for that delivery stage
            addingDeliveryStages.ForEach(pm => pm.TotalTypeMaterial = pm.PurchaseMaterials.Count);

            // Perform add data
            var incomingDeliveryStages = _mapper.Map<List<DeliveryStage>>(addingDeliveryStages);

            incomingDeliveryStages.ForEach(async x => await CheckDuplicateMaterialInDeliveryStage(x));
            
            foreach (var incomingDeliveryStage in incomingDeliveryStages)
            {
                existingPurchaseOrder.DeliveryStages.Add(incomingDeliveryStage);
            }

            // Validate data before save to database
            await CheckIncomingQuantityOfMaterial(existingPurchaseOrder);
            existingPurchaseOrder.NumOfDeliveryStage = existingPurchaseOrder.DeliveryStages.Count;

            _unitOfWork.PurchasingOrderRepo.Update(existingPurchaseOrder);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task UpdateDeliveryStagesAsync(List<DeliveryStageUpdateVM> updatingDeliveryStagesVM, int purchaseOrderId)
        {
            var existingPurchaseOrder = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(purchaseOrderId);

            if (existingPurchaseOrder == null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - This purchasing order does not exist");
            }

            if (existingPurchaseOrder.ManagerApproveStatus != ApproveEnum.Approved || existingPurchaseOrder.SupplierApproveStatus != ApproveEnum.Approved)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.REQUEST_NOT_APPROVED), ExceptionMessage.REQUEST_NOT_APPROVED);
            }

            var existedDeliveryStages = existingPurchaseOrder.DeliveryStages.ToList();

            foreach (var updatingDeliveryStage in updatingDeliveryStagesVM)
            {
                // Select existed delivery stage that need to be updated
                var existedDeliveryStage = existedDeliveryStages.FirstOrDefault(x => x.Id == updatingDeliveryStage.Id);

                foreach (var existedPurchaseMaterial in existedDeliveryStage.PurchaseMaterials)
                {
                    // Select existed purchase material that need to be updated
                    var updatingPurchaseMaterial = updatingDeliveryStage.PurchaseMaterials.FirstOrDefault(x => x.Id == existedPurchaseMaterial.Id);
                    if (updatingPurchaseMaterial != null)
                    {
                        _mapper.Map(updatingPurchaseMaterial, existedPurchaseMaterial);
                        existedPurchaseMaterial.TotalPrice = existedPurchaseMaterial.TotalQuantity * existedPurchaseMaterial.PackagePrice;
                    }
                }

                existedDeliveryStage.TotalPrice = existedDeliveryStage.PurchaseMaterials.Sum(x => x.TotalPrice.Value);
            }

            // Validate data before save to database
            existingPurchaseOrder.DeliveryStages.ToList().ForEach(async x => await CheckDuplicateMaterialInDeliveryStage(x));
            await CheckIncomingQuantityOfMaterial(existingPurchaseOrder);

            //TODO: Check if delivery stage has done, cannot update
            _unitOfWork.PurchasingOrderRepo.Update(existingPurchaseOrder);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        private async Task CheckIncomingQuantityOfMaterial(PurchasingOrder existedPurchasingOrder)
        {
            var orderMaterialList = existedPurchasingOrder.OrderMaterials.Where(x => x.IsDeleted == false);
            foreach (var orderedMaterial in orderMaterialList)
            {
                var orderdMaterialId = orderedMaterial.RawMaterialId;
                double totalCheckQuantity = 0;
                // calculate total from existing delivery stage
                foreach (var existingDeliveryStage in existedPurchasingOrder.DeliveryStages)
                {
                    foreach (var purchaseMaterial in existingDeliveryStage.PurchaseMaterials)
                    {
                        if (purchaseMaterial.RawMaterialId == orderdMaterialId)
                        {
                            totalCheckQuantity += purchaseMaterial.TotalQuantity.Value;
                        }
                    }
                    //var existedMaterialInPurchaseMaterial = await _unitOfWork.PurchaseMaterialRepo.GetPurchaseMaterialByDeliveryStageIdAndRawMaterialId(existingDeliveryStage.Id, orderdMaterialId.Value);
                    //if (existedMaterialInPurchaseMaterial != null)
                    //    totalCheckQuantity += existedMaterialInPurchaseMaterial.TotalQuantity.Value;
                }

                if (totalCheckQuantity > orderedMaterial.PackageQuantity)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Number of material in delivery stage cannot exceed the amount in purchasing order");
                }
            }
        }

        public async Task<List<DeliveryStageVM>> GetAllByPurchasingOrderIdAsync(int purchasingOrderId)
        {
            var items = await _unitOfWork.DeliveryStageRepo.GetAllByPurchasingOrderIdAsync(purchasingOrderId);
            var result = _mapper.Map<List<DeliveryStageVM>>(items);
            return result;
        }

        private async Task CheckDuplicateMaterialInDeliveryStage(DeliveryStage deliveryStage)
        {
            var duplicateId = deliveryStage.PurchaseMaterials
                .Where(x => x.IsDeleted == false)
                .GroupBy(e => e.RawMaterialId)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            if (duplicateId.Count() > 0)
            {
                var idString = "";
                foreach (var materialId in duplicateId)
                {
                    idString += materialId + ", ";
                }

                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Duplicate material id {idString} in delivery stage");
            }
        }

        public async Task<DeliveryStageVM> GetByTempWarehouseRequest(int requestId)
        {
            var tempWarehouseRequest = await _unitOfWork.TempWarehouseRequestRepo.GetByIdAsync(requestId);
            if (tempWarehouseRequest == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + " - Temp warehouse request");

            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(tempWarehouseRequest.DeliveryStageId);

            if (deliveryStage == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<DeliveryStageVM>(deliveryStage);
            return result;
        }

        public async Task<DeliveryStageVM> GetByImportMainWarehouseRequest(int requestId)
        {
            var importWarehouseRequest = await _unitOfWork.ImportMainWarehouseRequestRepo.GetByIdAsync(requestId);
            if (importWarehouseRequest == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + " - Import main warehouse request");

            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(importWarehouseRequest.DeliveryStageId);

            if (deliveryStage == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<DeliveryStageVM>(deliveryStage);
            return result;
        }

        public async Task<DeliveryStageVM> GetByInspectionRequest(int requestId)
        {
            var inspectionRequest = await _unitOfWork.InspectionRequestRepo.GetByIdAsync(requestId);
            if (inspectionRequest == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + " - Inspection warehouse request");

            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(inspectionRequest.DeliveryStageId);

            if (deliveryStage == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<DeliveryStageVM>(deliveryStage);
            return result;
        }

        public async Task ChangeDeliveryStageQuantityAsync(DeliveryStageUpdateQuantityVM updateQuantityVM)
        {
            var existingDeliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(updateQuantityVM.Id);
            var deliveryStageList = await _unitOfWork.DeliveryStageRepo.GetAllByPurchasingOrderIdAsync(existingDeliveryStage.PurchasingOrderId.Value);
            var subDs = deliveryStageList.FirstOrDefault(x => x.IsSupplemental == true);

            

            if (existingDeliveryStage.DeliveryStatus != DeliveryStageStatusEnum.Delivering)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_APPROVAL_STATUS), ExceptionMessage.INVALID_APPROVAL_STATUS + " - You can only set the quantity when it is delivering");
            }
            foreach (var dto in updateQuantityVM.PurchaseMaterials)
            {
                var existingPurchaseMaterial = existingDeliveryStage.PurchaseMaterials.FirstOrDefault(x => x.Id == dto.Id);
                if (existingPurchaseMaterial != null)
                {
                    // If this is supplemental delivery stage, the deliver quantity must equal to total quantity
                    if (existingDeliveryStage.IsSupplemental && existingPurchaseMaterial.TotalQuantity > dto.DeliveredQuantity)
                    {
                        var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(existingPurchaseMaterial.RawMaterialId);
                        throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Delivered quantity for raw material {rawMaterial.Name} must be greater than total quantity in supplemental delivery stage");
                    }

                    existingPurchaseMaterial.DeliveredQuantity = dto.DeliveredQuantity;
                }
                if (!existingDeliveryStage.IsSupplemental)
                {
                    if (existingPurchaseMaterial.TotalQuantity > existingPurchaseMaterial.DeliveredQuantity)
                    {
                        var supplementMaterial = subDs.PurchaseMaterials.FirstOrDefault(x => x.RawMaterialId == existingPurchaseMaterial.RawMaterialId);
                        supplementMaterial.TotalQuantity += existingPurchaseMaterial.TotalQuantity - existingPurchaseMaterial.DeliveredQuantity;
                        supplementMaterial.TotalPrice = supplementMaterial.TotalQuantity * supplementMaterial.PackagePrice;
                    }
                    if (existingPurchaseMaterial.TotalQuantity < existingPurchaseMaterial.DeliveredQuantity)
                    {
                        var supplementMaterial = subDs.PurchaseMaterials.FirstOrDefault(x => x.RawMaterialId == existingPurchaseMaterial.RawMaterialId);
                        supplementMaterial.TotalQuantity -= existingPurchaseMaterial.DeliveredQuantity - existingPurchaseMaterial.TotalQuantity;
                        supplementMaterial.TotalPrice = supplementMaterial.TotalQuantity * supplementMaterial.PackagePrice;
                    }
                }
                
            }

            existingDeliveryStage.DeliveryStatus = DeliveryStageStatusEnum.Delivered;
            existingDeliveryStage.PurchaseMaterials
                .Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive)
                .ToList().ForEach(x => x.WarehouseStatus = DeliveryStageStatusEnum.Delivered);

            _unitOfWork.DeliveryStageRepo.Update(subDs);
            _unitOfWork.DeliveryStageRepo.Update(existingDeliveryStage);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task PerformDeliveringSupplementalDeliveryStage(DeliveryStageSuppStartDeliveringVM deliveringState)
        {
            var existingDeliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(deliveringState.DeliveryStageId);

            if (existingDeliveryStage == null)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (!existingDeliveryStage.IsSupplemental)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Not a supplemental delivery stage");

            if (existingDeliveryStage.DeliveryStatus != DeliveryStageStatusEnum.Approved)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Delivery stage is not approved");

            if (existingDeliveryStage.DeliveryDate < DateTime.Today)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Supplemental delivery stage should be now");
            existingDeliveryStage.DeliveryStatus = DeliveryStageStatusEnum.Delivering;

            foreach (var purchaseMaterial in existingDeliveryStage.PurchaseMaterials)
            {
                if (purchaseMaterial.WarehouseStatus != DeliveryStageStatusEnum.SupInactive)
                {
                    purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.Delivering;
                    existingDeliveryStage.DeliveryDate = deliveringState.DeliveryDate;
                }
            }
            _unitOfWork.DeliveryStageRepo.Update(existingDeliveryStage);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelDeliveryStage(int deliveryStageId)
        {
            var existingDeliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(deliveryStageId);
            var deliveryStageListInPO = await _unitOfWork.DeliveryStageRepo.GetAllByPurchasingOrderIdAsync(existingDeliveryStage.PurchasingOrderId.Value);

            var supplementalDeliveryStage = deliveryStageListInPO.FirstOrDefault(x => x.IsSupplemental);

            if (existingDeliveryStage == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (existingDeliveryStage.DeliveryStatus != DeliveryStageStatusEnum.Approved
                && existingDeliveryStage.DeliveryStatus != DeliveryStageStatusEnum.Delivering)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - You can only cancel approve or delivering delivery stage");


            if (existingDeliveryStage.IsSupplemental)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - You cannot cancel supplemental delivery stage");

            existingDeliveryStage.DeliveryStatus = DeliveryStageStatusEnum.Cancelled;

            foreach (var purchaseMaterial in existingDeliveryStage.PurchaseMaterials)
            {
                purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.Cancelled;

                var supplementMaterial = supplementalDeliveryStage.PurchaseMaterials.FirstOrDefault(x => x.RawMaterialId == purchaseMaterial.RawMaterialId);
                supplementMaterial.TotalQuantity += purchaseMaterial.TotalQuantity;
            }

            _unitOfWork.DeliveryStageRepo.Update(existingDeliveryStage);

            _unitOfWork.DeliveryStageRepo.Update(supplementalDeliveryStage);
            await _unitOfWork.SaveChangesAsync();

            // Check if all delivery stage in PO is cancelled, then create sub delivery stage
            var po = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(existingDeliveryStage.PurchasingOrderId.Value);
            var subDs = po.DeliveryStages.FirstOrDefault(x => x.IsSupplemental);

            if (po.DeliveryStages.Where(x => !x.IsSupplemental).All(y =>
                y.DeliveryStatus == DeliveryStageStatusEnum.MainWarehouseImported
                || y.DeliveryStatus == DeliveryStageStatusEnum.Cancelled))
            {
                if (subDs.PurchaseMaterials.Any(x => x.TotalQuantity > 0) && subDs.DeliveryStatus == DeliveryStageStatusEnum.SupInactive)
                {
                    subDs.DeliveryStatus = DeliveryStageStatusEnum.Approved;
                    foreach (var item in subDs.PurchaseMaterials)
                    {
                        if (item.TotalQuantity > 0)
                        {               
                            item.TotalPrice = item.TotalQuantity * item.PackagePrice;
                            item.WarehouseStatus = DeliveryStageStatusEnum.Approved;
                        }
                    }
                }

                subDs.TotalTypeMaterial = subDs
                    .PurchaseMaterials
                    .Where(x => x.TotalQuantity.Value > 0)
                    .Count();
                subDs.TotalPrice = subDs
                    .PurchaseMaterials
                    .Where(x => x.TotalQuantity.Value > 0)
                    .Sum(x => x.TotalPrice.Value);
            }
            _unitOfWork.DeliveryStageRepo.Update(subDs);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);

        }
    }
}
