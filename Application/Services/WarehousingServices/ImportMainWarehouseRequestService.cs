using Application.Exceptions;
using Application.IServices;
using Application.IServices.WarehousingServices;
using Application.ViewModels.InspectionRequest;
using Application.ViewModels.MainWarehouse;
using Application.ViewModels.TempWarehouse;
using AutoMapper;
using Domain.Entities.Warehousing;
using Domain.Enums;
using Domain.Enums.DeliveryStage;
using Domain.Enums.Warehousing;
using System.Net;

namespace Application.Services.WarehousingServices
{
    public class ImportMainWarehouseRequestService : IImportMainWarehouseRequestService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        private readonly IClaimsService _claimsService;
        private readonly IDeliveryStageService _deliveryStageService;

        public ImportMainWarehouseRequestService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService, IDeliveryStageService deliveryStageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
            _deliveryStageService = deliveryStageService;
        }


        public async Task<List<ImportMainWarehouseRequestVM>> GetAllByDeliveryStageIdAsync(int deliveryStageId)
        {
            var itemList = await _unitOfWork.ImportMainWarehouseRequestRepo.GetAllByDeliveryStageIdAsync(deliveryStageId);
            var result = _mapper.Map<List<ImportMainWarehouseRequestVM>>(itemList);

            return result;
        }

        public async Task CreateAsync(ImportMainWarehouseRequestAddVM mainWarehouseRequestDTO)
        {
            if (mainWarehouseRequestDTO.RequestExecutionDate < DateTime.Today)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Request ExecutionDate Cannot Be Before Today");
            }
            var createItem = _mapper.Map<ImportMainWarehouseRequest>(mainWarehouseRequestDTO);

            // Get the current login user id and check if it is warehouse staff
            var requesInspector = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);

            if (requesInspector != null && requesInspector.InspectorId != null)
            {
                var requestStaffId = requesInspector.InspectorId;
                createItem.InspectorId = requestStaffId.Value;
            }
            else
            {
                throw new APIException(HttpStatusCode.Unauthorized, nameof(ExceptionMessage.USER_NOT_ALLOWED), ExceptionMessage.USER_NOT_ALLOWED);
            }

            // Get the current delivery stage
            var checkDeliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdAsync(mainWarehouseRequestDTO.DeliveryStageId);
            if (checkDeliveryStage == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }

            var deliveryStageObj = await _unitOfWork.DeliveryStageRepo.GetByIdWithPO(mainWarehouseRequestDTO.DeliveryStageId);
            createItem.POCode = deliveryStageObj.PurchasingOrder.POCode;

            // Throw exception if delivery stage is exported from temp warehouse
            if (checkDeliveryStage.DeliveryStatus != DeliveryStageStatusEnum.TempWarehouseExported)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTAVAILABLE), ExceptionMessage.DELIVERYSTAGE_NOTAVAILABLE);
            }

            await _unitOfWork.ImportMainWarehouseRequestRepo.AddAsync(createItem);

            // Set status after create warehouse request
            // update delivery status after create warehouse request
            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(createItem.DeliveryStageId);
            deliveryStage.DeliveryStatus = DeliveryStageStatusEnum.MainWarehouseImportPending;
            foreach (var item in deliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                item.WarehouseStatus = DeliveryStageStatusEnum.MainWarehouseImportPending;
            }

            _unitOfWork.DeliveryStageRepo.Update(deliveryStage);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task UpdateApproveStatus(ImportMainWarehouseApproveRequestVM approveRequestDTO)
        {
            var approveItem = await _unitOfWork.ImportMainWarehouseRequestRepo.GetByIdAsync(approveRequestDTO.Id);

            if (approveItem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (approveItem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }
            _mapper.Map(approveRequestDTO, approveItem);

            // Set approve user id
            var approveStaff = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);
            if (approveStaff != null && approveStaff.WarehouseStaffId != null)
            {
                var approveStaffId = approveStaff.WarehouseStaffId;
                approveItem.ApproveWStaffId = approveStaffId;
            }
            else
            {
                throw new APIException(HttpStatusCode.Unauthorized, nameof(ExceptionMessage.USER_NOT_ALLOWED), ExceptionMessage.USER_NOT_ALLOWED);
            }

            _unitOfWork.ImportMainWarehouseRequestRepo.Update(approveItem);

            var ds = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(approveItem.DeliveryStageId);
            // Update status after approve
            if (approveItem.ApproveStatus == ApproveEnum.Approved)
            {
                ds.DeliveryStatus = DeliveryStageStatusEnum.MainWarehouseImportAprroved;
                foreach (var purchaseMaterial in ds.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                {
                    purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.MainWarehouseImportAprroved;
                }

            }
            else if (approveItem.ApproveStatus == ApproveEnum.Rejected)
            {
                ds.DeliveryStatus = DeliveryStageStatusEnum.TempWarehouseExported;
                foreach (var purchaseMaterial in ds.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                {
                    purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.TempWarehouseExported;
                }
            }

            _unitOfWork.DeliveryStageRepo.Update(ds);

            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task UpdateAsync(ImportMainWarehouseRequestUpdateVM requestDTO)
        {
            var updateItem = await _unitOfWork.ImportMainWarehouseRequestRepo.GetByIdAsync(requestDTO.Id);
            if (updateItem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (updateItem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }
            _mapper.Map(requestDTO, updateItem);
            _unitOfWork.ImportMainWarehouseRequestRepo.Update(updateItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.REQUEST_NOTEXIST), ExceptionMessage.REQUEST_NOTEXIST);
        }

        public async Task DeleteAsync(int id)
        {
            var deleteitem = await _unitOfWork.ImportMainWarehouseRequestRepo.GetByIdAsync(id);
            if (deleteitem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUEST_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (deleteitem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }

            _unitOfWork.ImportMainWarehouseRequestRepo.SoftRemove(deleteitem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task<List<ImportMainWarehouseRequestVM>> GetAllAsync()
        {
            var items = await _unitOfWork.ImportMainWarehouseRequestRepo.GetAllWithDetailAsync();
            var result = _mapper.Map<List<ImportMainWarehouseRequestVM>>(items);
            return result;
        }

        public async Task<ImportMainWarehouseRequestVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.ImportMainWarehouseRequestRepo.GetByIdWithFormAsync(id);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<ImportMainWarehouseRequestVM>(item);
            return result;
        }
    }
}
