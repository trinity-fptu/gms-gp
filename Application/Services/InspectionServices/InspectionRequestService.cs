using Application.Exceptions;
using Application.IServices;
using Application.IServices.IInspectionServices;
using Application.ViewModels.InspectionRequest;
using Application.ViewModels.MainWarehouse;
using Application.ViewModels.TempWarehouse;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entities.Inspect;
using Domain.Entities.Warehousing;
using Domain.Enums;
using Domain.Enums.DeliveryStage;
using System.Net;

namespace Application.Services.InspectionServices
{
    public class InspectionRequestService : IInspectionRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        private readonly IDeliveryStageService _deliveryStageService;

        public InspectionRequestService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService, IDeliveryStageService deliveryStageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
            _deliveryStageService = deliveryStageService;
        }

        public async Task CreateAsync(InspectionRequestAddVM inspectionRequestVM)
        {
            if(inspectionRequestVM.RequestInspectDate.Value.Date < DateTime.Today)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + "Request Inspectdate Cannot Be Before Today");
            }
            var createItem = _mapper.Map<InspectionRequest>(inspectionRequestVM);

            // Get the current login user id and check if it is purchasing staff
            var requestStaff = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);
            if (requestStaff != null)
            {
                var requestStaffId = requestStaff.PurchasingStaffId;
                createItem.RequestStaffId = requestStaffId.Value;
            }
            else
            {
                throw new APIException(HttpStatusCode.Unauthorized, nameof(ExceptionMessage.USER_NOT_ALLOWED), ExceptionMessage.USER_NOT_ALLOWED);
            }

            // Get the current delivery stage
            var checkDeliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdAsync(inspectionRequestVM.DeliveryStageId.Value);
            if (checkDeliveryStage == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }

            var deliveryStageObj = await _unitOfWork.DeliveryStageRepo.GetByIdWithPO(inspectionRequestVM.DeliveryStageId.Value);
            createItem.POCode = deliveryStageObj.PurchasingOrder.POCode;

            // Throw exception if delivery stage is not imported to temp warehouse
            if (checkDeliveryStage.DeliveryStatus != DeliveryStageStatusEnum.TempWarehouseImported)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTAVAILABLE), ExceptionMessage.DELIVERYSTAGE_NOTAVAILABLE);
            }

            await _unitOfWork.InspectionRequestRepo.AddAsync(createItem);
            // Set status after create inspection request
            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(createItem.DeliveryStageId);
            if (deliveryStage == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }

            deliveryStage.DeliveryStatus = DeliveryStageStatusEnum.PendingForInspection;
            foreach (var material in deliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                material.WarehouseStatus = DeliveryStageStatusEnum.PendingForInspection;
            }
            _unitOfWork.DeliveryStageRepo.Update(deliveryStage);

            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var deleteitem = await _unitOfWork.InspectionRequestRepo.GetByIdAsync(id);
            if (deleteitem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUEST_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (deleteitem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }

            _unitOfWork.InspectionRequestRepo.SoftRemove(deleteitem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }

        public async Task<List<InspectionRequestVM>> GetAllAsync()
        {
            var items = await _unitOfWork.InspectionRequestRepo.GetAllWithDetailAsync();
            var result = _mapper.Map<List<InspectionRequestVM>>(items);
            return result;
        }

        public async Task<List<InspectionRequestVM>> GetAllByDeliveryStageIdAsync(int deliveryStageId)
        {
            var itemList = await _unitOfWork.InspectionRequestRepo.GetAllByDeliveryStageIdAsync(deliveryStageId);
            var result = _mapper.Map<List<InspectionRequestVM>>(itemList);

            return result;
        }

        public async Task<InspectionRequestVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.InspectionRequestRepo.GetByIdWithInspectionFormAsync(id);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<InspectionRequestVM>(item);
            result.DeliveryStage.PurchaseMaterials.RemoveAll(x => x.TotalQuantity <= 0);

            return result;
        }

        public async Task UpdateApproveStatus(InspectionApproveRequestVM approveRequestVM)
        {
            var approveItem = await _unitOfWork.InspectionRequestRepo.GetByIdAsync(approveRequestVM.Id);

            if (approveItem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (approveItem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }
            
            _mapper.Map(approveRequestVM, approveItem);

            if (approveItem.ApprovedDate < approveItem.RequestInspectDate)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + "Approved Date Cannot Be Before Request Inspect Date");
            }

            // Set approve user id
            var approveInspector = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);
            if (approveInspector != null)
            {
                var approveInspectorId = approveInspector.InspectorId;
                approveItem.ApprovingInspectorId = approveInspectorId;
            }
            else
            {
                throw new APIException(HttpStatusCode.Unauthorized, nameof(ExceptionMessage.USER_NOT_ALLOWED), ExceptionMessage.USER_NOT_ALLOWED);
            }

            _unitOfWork.InspectionRequestRepo.Update(approveItem);

            // Set status after create inspection request
            var deliveryStageStatus = DeliveryStageStatusEnum.TempWarehouseImported;
            if (approveItem.ApproveStatus == ApproveEnum.Approved)
            {
                deliveryStageStatus = DeliveryStageStatusEnum.InspectionRequestAprroved;
            }

            var deliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(approveItem.DeliveryStageId);
            if (deliveryStage == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }

            deliveryStage.DeliveryStatus = deliveryStageStatus;
            foreach (var material in deliveryStage.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
            {
                material.WarehouseStatus = deliveryStageStatus;
            }
            _unitOfWork.DeliveryStageRepo.Update(deliveryStage);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.REQUEST_NOTEXIST), ExceptionMessage.REQUEST_NOTEXIST);

        }

        public async Task UpdateAsync(InspectionRequestUpdateVM requestDTO)
        {
            // find existing inspect request
            var updateItem = await _unitOfWork.InspectionRequestRepo.GetByIdAsync(requestDTO.Id);
            if (updateItem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (updateItem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }

            _mapper.Map(requestDTO, updateItem);
            _unitOfWork.InspectionRequestRepo.Update(updateItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.REQUEST_NOTEXIST), ExceptionMessage.REQUEST_NOTEXIST);
        }
    }
}