using Application.Exceptions;
using Application.IServices;
using Application.Utils;
using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Application.ViewModels.PurchasingOrder;
using Application.ViewModels.PurchasingOrder.OrderMaterial;
using Application.ViewModels.PurchasingPlan;
using Application.ViewModels.PurchasingTask;
using Application.ViewModels.RawMaterial;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Enums.DeliveryStage;
using System.Net;
using System.Xml.Linq;

namespace Application.Services
{
    public class PurchasingOrderService : IPurchasingOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public PurchasingOrderService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task CreateAsync(PurchasingOrderAddVM model)
        {
            // Validate information
            List<DeliveryStage> includeSubDS = new List<DeliveryStage>();
            var existedPurchasingPlan = await _unitOfWork.PurchasingPlanRepo.GetByIdWithDetailAsync(model.PurchasingPlanId.Value);
            if (existedPurchasingPlan == null)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - This purchasing plan does not exist");
            }
            if (existedPurchasingPlan.ApproveStatus != ApproveEnum.Approved)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_APPROVAL_STATUS), ExceptionMessage.INVALID_APPROVAL_STATUS + " - This purchasing plan is not approved");
            }
            if (existedPurchasingPlan.ProcessStatus == ProcessStatus.Overdue)
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_APPROVAL_STATUS), ExceptionMessage.INVALID_APPROVAL_STATUS + " - This purchasing plan is overdue");
            }

            foreach (var item in model.OrderMaterials)
            {
                var purchasingTasks = existedPurchasingPlan.PurchaseTasks;

                var taskObj = purchasingTasks.FirstOrDefault(x => x.RawMaterialId == item.RawMaterialId);
                if (taskObj == null)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_APPROVAL_STATUS), ExceptionMessage.INVALID_APPROVAL_STATUS + " - Selected task is not found");
                }
                // Check if there is any overdue task selected in the purchasing order    
                if (taskObj.TaskStatus == PurchasingTaskEnum.PurchasingTaskStatus.Overdue 
                    || taskObj.TaskStatus == PurchasingTaskEnum.PurchasingTaskStatus.Pending)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_APPROVAL_STATUS), ExceptionMessage.INVALID_APPROVAL_STATUS + " - Selected task is not available to create purchase order");
                }

                // Check if there is any overdue task selected in the purchasing order    
                var user = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);

                if (taskObj.PurchasingStaffId != user.PurchasingStaffId)
                {
                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_APPROVAL_STATUS), ExceptionMessage.INVALID_APPROVAL_STATUS + " - Selected task is not assigned to this user");
                }
            }

            // Calculate total price of each material
            model.OrderMaterials.ForEach(x => x.TotalPrice = x.PackagePrice * x.PackageQuantity);
            //model.DeliveryStages.ForEach(x => x.PurchaseMaterials.ForEach(y => y.TotalPrice = y.PackagePrice * y.TotalQuantity));
            //model.DeliveryStages.ForEach(x => x.TotalPrice = x.PurchaseMaterials.Sum(x => x.TotalQuantity));

            // Calculate total price of the order
            model.TotalPrice = model.OrderMaterials.Sum(x => x.TotalPrice);

            // Count number of materials in the order
            model.TotalMaterialType = model.OrderMaterials.Count;

            // Create new purchasing order
            var createdItem = _mapper.Map<PurchasingOrder>(model);
            await CheckDuplicateMaterialInPurchasingOrder(createdItem);


            //createdItem.ReceiverCompanyAddress = "Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh";
            //createdItem.ReceiverCompanyEmail = "mpms@mail.com";
            //createdItem.ReceiverCompanyPhone = "0985634212";

            var createUser = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);
            if (createUser != null && createUser.PurchasingStaffId != null)
                createdItem.PurchasingStaffId = createUser.PurchasingStaffId.Value;
            else
            {
                throw new APIException(HttpStatusCode.Unauthorized, nameof(ExceptionMessage.USER_NOT_ALLOWED), ExceptionMessage.USER_NOT_ALLOWED);
            }

            if (createdItem.DeliveryStages.Any(x => x.DeliveryDate.Value.Date < DateTime.Today))
            {
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Invalid delivery date");
            }

            await CheckQuantityWithExistingPurchasingPlan(createdItem);
            await CheckIncomingQuantityOfMaterial(createdItem);
            createdItem.POCode = await GeneratePOCode(createdItem);

            // set unit name and material code for purchase material
            var order = 1;
            foreach (var deliveryStage in createdItem.DeliveryStages)
            {
                deliveryStage.StageOrder = order++;
                if(deliveryStage.PurchaseMaterials != null)
                {
                    deliveryStage.TotalTypeMaterial = deliveryStage.PurchaseMaterials.Count;
                }
                
                foreach (var purchaseMaterial in deliveryStage.PurchaseMaterials)
                {
                    var material = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                    if (material != null)
                    {
                        var orderMaterial = createdItem.OrderMaterials.FirstOrDefault(x => x.RawMaterialId == purchaseMaterial.RawMaterialId);
                        purchaseMaterial.PackagePrice = orderMaterial.PackagePrice;
                        purchaseMaterial.Unit = material.Unit.Value;
                        purchaseMaterial.Package = material.Package;
                        purchaseMaterial.MaterialPerPackage = orderMaterial.MaterialPerPackage;
                        purchaseMaterial.Code = $"PurM-{StringUtils.GenerateRandomNumberString(8)}";

                        purchaseMaterial.TotalPrice = purchaseMaterial.PackagePrice * purchaseMaterial.TotalQuantity;
                        purchaseMaterial.CompanyMaterialCode = material.Code;
                        purchaseMaterial.MaterialName = material.Name;
                    }
                }
                deliveryStage.TotalPrice = deliveryStage.PurchaseMaterials.Sum(x => x.TotalPrice.Value);
                includeSubDS.Add(deliveryStage);
            }

            createdItem.NumOfDeliveryStage = includeSubDS.Count;

            await AddProcessQuantityToPurchasingTasks(createdItem);
            DeliveryStage subDs = new DeliveryStage();
            subDs.StageOrder = createdItem.NumOfDeliveryStage + 1;
            subDs.CreatedDate = createdItem.CreatedDate;
            subDs.IsSupplemental = true;

            subDs.DeliveryStatus = DeliveryStageStatusEnum.SupInactive;
            List<PurchaseMaterial> subDSListPS = new List<PurchaseMaterial>();
            foreach (var ordermaterial in createdItem.OrderMaterials)
            {
                var raw = await _unitOfWork.RawMaterialRepo.GetByIdAsync(ordermaterial.RawMaterialId);
                PurchaseMaterial subMaterial = new PurchaseMaterial();
                subMaterial.MaterialName = ordermaterial.MaterialName;
                subMaterial.Code = $"PurM-{StringUtils.GenerateRandomNumberString(8)}";
                subMaterial.CompanyMaterialCode = raw.Code;
                subMaterial.MaterialPerPackage = ordermaterial.MaterialPerPackage;
                subMaterial.Package = raw.Package;
                subMaterial.RawMaterialId = ordermaterial.RawMaterialId;
                subMaterial.Unit = raw.Unit.Value;
                subMaterial.PackagePrice = ordermaterial.PackagePrice;
                subMaterial.TotalQuantity = 0;
                subMaterial.TotalPrice = 0;
                subMaterial.WarehouseStatus = DeliveryStageStatusEnum.SupInactive;
                subDSListPS.Add(subMaterial);
            }
            subDs.PurchaseMaterials = subDSListPS;
            includeSubDS.Add(subDs);
            createdItem.DeliveryStages = includeSubDS;
            createdItem.OrderStatus = Domain.Enums.PurchasingOrder.PurchasingOrderStatusEnum.Pending;
            await _unitOfWork.PurchasingOrderRepo.AddAsync(createdItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task ApproveAsync(ApproveEnum approvingStatus, int id, bool isSupplier)
        {
            var itemToApprove = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(id);
            if (itemToApprove == null) throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (itemToApprove.SupplierApproveStatus == ApproveEnum.Approved && itemToApprove.ManagerApproveStatus == ApproveEnum.Approved)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);

            if (isSupplier) itemToApprove.SupplierApproveStatus = approvingStatus;
            else itemToApprove.ManagerApproveStatus = approvingStatus;

            if (itemToApprove.SupplierApproveStatus == ApproveEnum.Rejected || itemToApprove.ManagerApproveStatus == ApproveEnum.Rejected)
            {
                await ReduceProcessQuantityToPurchasingTasks(itemToApprove);
                itemToApprove.OrderStatus = Domain.Enums.PurchasingOrder.PurchasingOrderStatusEnum.Rejected;
            }

            if (itemToApprove.SupplierApproveStatus == ApproveEnum.Approved && itemToApprove.ManagerApproveStatus == ApproveEnum.Approved)
            {
                itemToApprove.OrderStatus = Domain.Enums.PurchasingOrder.PurchasingOrderStatusEnum.Processing;

                foreach (var deliveryStage in itemToApprove.DeliveryStages)
                {
                    if (!deliveryStage.IsSupplemental)
                    {
                        deliveryStage.DeliveryStatus = DeliveryStageStatusEnum.Approved;
                        foreach (var purchaseMaterial in deliveryStage.PurchaseMaterials)
                        {
                            purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.Approved;
                        }
                    }                    
                }
            }

            var purchasingPlan = await _unitOfWork.PurchasingPlanRepo.GetByIdWithDetailAsync(itemToApprove.PurchasingPlanId);

            foreach (var purchasingTask in purchasingPlan.PurchaseTasks)
            {
                foreach (var orderMaterial in itemToApprove.OrderMaterials)
                {
                    if (purchasingTask.RawMaterialId == orderMaterial.RawMaterialId)
                    {
                        purchasingTask.TaskStatus = PurchasingTaskEnum.PurchasingTaskStatus.Processing;
                        _unitOfWork.PurchasingTaskRepo.Update(purchasingTask);
                    }
                }
            }

            _unitOfWork.PurchasingOrderRepo.Update(itemToApprove);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(id);
            if (itemToDelete == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (itemToDelete.SupplierApproveStatus == ApproveEnum.Approved || itemToDelete.ManagerApproveStatus == ApproveEnum.Approved)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);
            if (itemToDelete.SupplierApproveStatus == ApproveEnum.Pending && itemToDelete.ManagerApproveStatus == ApproveEnum.Pending)
            {
                await ReduceProcessQuantityToPurchasingTasks(itemToDelete);
            }
            
            _unitOfWork.PurchasingOrderRepo.SoftRemove(itemToDelete);


            foreach (var orderMaterial in itemToDelete.OrderMaterials)
            {
                _unitOfWork.OrderMaterialRepo.SoftRemove(orderMaterial);
            }

            foreach (var deliveryStage in itemToDelete.DeliveryStages)
            {
                _unitOfWork.DeliveryStageRepo.SoftRemove(deliveryStage);
                foreach (var purchaseMaterial in deliveryStage.PurchaseMaterials)
                {
                    _unitOfWork.PurchaseMaterialRepo.SoftRemove(purchaseMaterial);
                }
            }
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task UpdateAsync(PurchasingOrderUpdateVM updateItem)
        {
            var existingItem = await _unitOfWork.PurchasingOrderRepo.GetByIdAsync(updateItem.Id);
            if (existingItem == null) throw new APIException(HttpStatusCode.NotFound,
                nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            if (existingItem.SupplierApproveStatus == ApproveEnum.Approved && existingItem.ManagerApproveStatus == ApproveEnum.Approved)
                throw new APIException(HttpStatusCode.BadRequest,
                                       nameof(ExceptionMessage.REQUEST_APPROVED), ExceptionMessage.REQUEST_APPROVED);

            

            _mapper.Map(updateItem, existingItem);
            existingItem.ManagerApproveStatus = ApproveEnum.Pending;
            existingItem.SupplierApproveStatus = ApproveEnum.Pending;

            _unitOfWork.PurchasingOrderRepo.Update(existingItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest,
                nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task<List<PurchasingOrderVM>> GetAllAsync()
        {
            var items = await _unitOfWork.PurchasingOrderRepo.GetAllWithDetailAsync();
            var result = _mapper.Map<List<PurchasingOrderVM>>(items);
            return result;
        }

        public async Task<List<PurchasingOrderVM>> GetAllAuthorizeAsync()
        {
            var userRole = _claimsService.GetCurrentRoleId;
            var userId = _claimsService.GetCurrentUserId;
            if (userRole == 1 || userRole == 7)
            {
                var items = await _unitOfWork.PurchasingOrderRepo.GetAllWithDetailAsync();
                var result = _mapper.Map<List<PurchasingOrderVM>>(items);
                return result;
            }
            if (userRole == 2 || userRole == 3 || userRole == 6)
            {
                List<PurchasingOrder> ListPo = new List<PurchasingOrder>();
                List<List<PurchasingOrder>> ListItem = new List<List<PurchasingOrder>>();
                var list1 = await _unitOfWork.PurchasingOrderRepo.GetByPmanagerId(userId);
                var list2 = await _unitOfWork.PurchasingOrderRepo.GetByPstaffId(userId);
                var list3 = await _unitOfWork.PurchasingOrderRepo.GetBySupplierId(userId);
                ListItem.Add(list1);
                ListItem.Add(list2);
                ListItem.Add(list3);
                foreach (var listObj in ListItem)
                {
                    if (listObj != null)
                    {
                        ListPo = listObj;
                        var result = _mapper.Map<List<PurchasingOrderVM>>(ListPo);
                        return result;
                    }
                }
            }
            throw new APIException(HttpStatusCode.Unauthorized,
            nameof(ExceptionMessage.USER_UNAUTHORIZE), ExceptionMessage.USER_UNAUTHORIZE);
        }

        public async Task<PurchasingOrderVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(id);
            if (item == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<PurchasingOrderVM>(item);
            result.DeliveryStages.RemoveAll(x => x.DeliveryStatus == DeliveryStageStatusEnum.SupInactive);
            result.DeliveryStages.ForEach(x => x.PurchaseMaterials.RemoveAll(y => y.WarehouseStatus == DeliveryStageStatusEnum.SupInactive));
            return result;
        }

        public async Task<List<PurchasingOrderVM>> GetAllByPurchasingPlanIdAsync(int purchasingPlanId)
        {
            var itemList = await _unitOfWork.PurchasingOrderRepo.GetAllWithDetailByPurchasingPlanIdAsync(purchasingPlanId);
            var result = _mapper.Map<List<PurchasingOrderVM>>(itemList);
            return result;
        }

        private async Task CheckDuplicateMaterialInPurchasingOrder(PurchasingOrder purchasingOrder)
        {
            var duplicateId = purchasingOrder.OrderMaterials
                .Where(x => x.IsDeleted == false)
                .GroupBy(e => e.RawMaterialId)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            if (duplicateId.Count() > 0)
            {
                var idString = "";
                foreach (var materialId in duplicateId)
                {
                    var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(materialId);
                    idString += $"id {materialId}, name \"{rawMaterial.Name}\"";
                }

                throw new APIException(HttpStatusCode.BadRequest,
                    nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Duplicate material {idString} in order materials");
            }

            foreach (var deliveryStage in purchasingOrder.DeliveryStages)
            {
                duplicateId = deliveryStage.PurchaseMaterials
                    .Where(x => x.IsDeleted == false)
                    .GroupBy(e => e.RawMaterialId)
                    .Where(g => g.Count() > 1)
                    .Select(x => x.Key);

                if (duplicateId.Count() > 0)
                {
                    var idString = "";
                    foreach (var materialId in duplicateId)
                    {
                        var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(materialId);
                        idString += $"id {materialId}, name \"{rawMaterial.Name}\"";
                    }

                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Duplicate material {idString} in purchasing material in a delivery stage");
                }
            }
        }

        public async Task<PurchasingOrderVM> GetByPOCodeAsync(string code)
        {
            var item = await _unitOfWork.PurchasingOrderRepo.GetByPOCodeAsync(code);

            if (item == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var result = _mapper.Map<PurchasingOrderVM>(item);
            return result;
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

                        if (!orderMaterialList.Any(x => x.RawMaterialId == purchaseMaterial.RawMaterialId))
                        {
                            var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(purchaseMaterial.RawMaterialId);
                            throw new APIException(HttpStatusCode.BadRequest,
                                nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Order material do not contain material with id {purchaseMaterial.RawMaterialId}, name \"{rawMaterial.Name}\", cannot add to delivery stage");
                        }
                    }
                }

                if (totalCheckQuantity != orderedMaterial.PackageQuantity)
                {
                    var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(orderedMaterial.RawMaterialId);
                    throw new APIException(HttpStatusCode.BadRequest,
                        nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Number of material with raw material id {orderdMaterialId}, name \"{rawMaterial.Name}\" in delivery stages ({totalCheckQuantity}) must equal the amount in order materials ({orderedMaterial.PackageQuantity})");
                }
            }
        }

        private async Task CheckQuantityWithExistingPurchasingPlan(PurchasingOrder inputPurchasingOrder)
        {
            var purchasingPlan = await _unitOfWork.PurchasingPlanRepo.GetByIdWithDetailAsync(inputPurchasingOrder.PurchasingPlanId);
            foreach (var orderMaterial in inputPurchasingOrder.OrderMaterials)
            {
                var purchasingTask = purchasingPlan.PurchaseTasks.FirstOrDefault(x => x.RawMaterialId == orderMaterial.RawMaterialId);
                if (orderMaterial.PackageQuantity * orderMaterial.MaterialPerPackage > purchasingTask.RemainedQuantity)
                {
                    var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(orderMaterial.RawMaterialId);
                    throw new APIException(HttpStatusCode.BadRequest,
                       nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Number of processed material with name \"{rawMaterial.Name}\" in order material cannot exceed the remain quantity amount in purchasing task");
                }
                if (orderMaterial.PackageQuantity <= 0)
                {
                    var rawMaterial = await _unitOfWork.RawMaterialRepo.GetByIdAsync(orderMaterial.RawMaterialId);
                    throw new APIException(HttpStatusCode.BadRequest,
                       nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + $" - Number of processed material with name \"{rawMaterial.Name}\" in order material must be over 0");
                }
            }
        }

        private async Task<string> GeneratePOCode(PurchasingOrder purchasingOrder)
        {
            while (true)
            {
                var poCode = $"PO{StringUtils.GenerateRandomNumberString(6)}";
                var existedPurchasingOrder = await _unitOfWork.PurchasingOrderRepo.GetByPOCodeAsync(poCode);
                if (existedPurchasingOrder == null)
                {
                    return poCode;
                }
            }
        }

        private async Task ReduceProcessQuantityToPurchasingTasks(PurchasingOrder purchasingOrder)
        {
            var purchasingPlan = await _unitOfWork.PurchasingPlanRepo.GetByIdWithDetailAsync(purchasingOrder.PurchasingPlanId);

            foreach (var orderMaterials in purchasingOrder.OrderMaterials)
            {
                foreach (var purchasingTask in purchasingPlan.PurchaseTasks)
                {
                    if (orderMaterials.RawMaterialId == purchasingTask.RawMaterialId)
                    {
                        purchasingTask.ProcessedQuantity -= orderMaterials.PackageQuantity * orderMaterials.MaterialPerPackage;
                        purchasingTask.RemainedQuantity += orderMaterials.PackageQuantity * orderMaterials.MaterialPerPackage;
                        if (purchasingTask.RemainedQuantity == purchasingTask.Quantity)
                            purchasingTask.TaskStatus = PurchasingTaskEnum.PurchasingTaskStatus.Pending;
                    }
                }
            }
            _unitOfWork.PurchasingPlanRepo.Update(purchasingPlan);
        }

        private async Task AddProcessQuantityToPurchasingTasks(PurchasingOrder purchasingOrder)
        {
            var purchasingPlan = await _unitOfWork.PurchasingPlanRepo.GetByIdWithDetailAsync(purchasingOrder.PurchasingPlanId);

            foreach (var orderMaterials in purchasingOrder.OrderMaterials)
            {
                foreach (var purchasingTask in purchasingPlan.PurchaseTasks)
                {
                    if (orderMaterials.RawMaterialId == purchasingTask.RawMaterialId)
                    {
                        purchasingTask.TaskStatus = PurchasingTaskEnum.PurchasingTaskStatus.Processing;
                        purchasingTask.ProcessedQuantity += orderMaterials.PackageQuantity * orderMaterials.MaterialPerPackage;
                        purchasingTask.RemainedQuantity -= orderMaterials.PackageQuantity * orderMaterials.MaterialPerPackage;
                    }
                }
            }
            _unitOfWork.PurchasingPlanRepo.Update(purchasingPlan);
        }

        public async Task<List<PurchasingOrderVM>> GetAllByPurchasingTaskIdAsync(int purchasingTaskId)
        {
            var purchasingTask = await _unitOfWork.PurchasingTaskRepo.GetByIdAsync(purchasingTaskId);
            if (purchasingTask == null)
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            var purchasingOrderList = await _unitOfWork.PurchasingOrderRepo.GetAllWithDetailByPurchasingPlanIdAsync(purchasingTask.PurchasingPlanId);

            var purchasingOrderListByPurchasingTask = purchasingOrderList.Where(po => po.OrderMaterials.Any(om => om.RawMaterialId == purchasingTask.RawMaterialId));

            var result = _mapper.Map<List<PurchasingOrderVM>>(purchasingOrderListByPurchasingTask);
            return result;
        }
    }
}
