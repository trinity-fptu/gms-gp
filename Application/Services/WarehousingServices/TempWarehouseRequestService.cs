using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.IServices.WarehousingServices;
using Application.ViewModels.TempWarehouse;
using AutoMapper;
using Domain.Entities.Warehousing;
using Domain.Enums.DeliveryStage;
using Domain.Enums;
using Application.ViewModels.MaterialCategory;
using Domain.Entities;
using Domain.Enums.Warehousing;
using Application.ViewModels.ProductionPlan;
using Application.IServices;

namespace Application.Services.WarehousingServices
{
    public class TempWarehouseRequestService : ITempWarehouseRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        private readonly IDeliveryStageService _deliveryStageService;
        public TempWarehouseRequestService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService, IDeliveryStageService deliveryStageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
            _deliveryStageService = deliveryStageService;
        }

        public async Task CreateAsync(TempWarehouseRequestAddVM TempWarehouseRequestDTO)
        {
            if (TempWarehouseRequestDTO.RequestExecutionDate.Value.Date < DateTime.Today)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Request Execution Date Cannot Be Before Today");
            }
            var createItem = _mapper.Map<TempWarehouseRequest>(TempWarehouseRequestDTO);

            // Set create staff id for warehouse request
            var requestStaff = await _unitOfWork.UserRepo.GetByIdAsync(_claimsService.GetCurrentUserId);

            if (requestStaff != null && requestStaff.PurchasingStaffId != null)
            {
                var requestStaffId = requestStaff.PurchasingStaffId;
                createItem.RequestStaffId = requestStaffId;
            }
            else if (requestStaff != null && requestStaff.InspectorId != null)
            {
                var requestInspectorId = requestStaff.InspectorId;
                createItem.RequestInspectorId = requestInspectorId;
            }
            else
            {
                throw new APIException(HttpStatusCode.Unauthorized, nameof(ExceptionMessage.USER_NOT_ALLOWED), ExceptionMessage.USER_NOT_ALLOWED);
            }

            // Check if the delivery stage is exist
            var checkDeliveryStage = await _unitOfWork.DeliveryStageRepo.GetByIdWithPO(createItem.DeliveryStageId);
            if (checkDeliveryStage == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            createItem.POCode = checkDeliveryStage.PurchasingOrder.POCode;
            createItem.RequestDate = DateTime.Today;

            // Check if the delivery stage status is delivered when request type is import
            if ((checkDeliveryStage.DeliveryStatus != DeliveryStageStatusEnum.Delivered)
                && (TempWarehouseRequestDTO.RequestType == WarehouseRequestTypeEnum.Import))
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTAVAILABLE), ExceptionMessage.DELIVERYSTAGE_NOTAVAILABLE);
            }
            // Check if the delivery stage status is TempWarehouseImported when request type is import
            else if ((checkDeliveryStage.DeliveryStatus != DeliveryStageStatusEnum.Inspected)
                && (TempWarehouseRequestDTO.RequestType == WarehouseRequestTypeEnum.Export))
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTAVAILABLE), ExceptionMessage.DELIVERYSTAGE_NOTAVAILABLE);
            }

            await _unitOfWork.TempWarehouseRequestRepo.AddAsync(createItem);

            if (TempWarehouseRequestDTO.RequestType == WarehouseRequestTypeEnum.Import)
            {
                var ds = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(createItem.DeliveryStageId);
                ds.DeliveryStatus = DeliveryStageStatusEnum.TempWarehouseImportPending;
                foreach (var purchaseMaterial in ds.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                {
                    purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.TempWarehouseImportPending;
                }
                _unitOfWork.DeliveryStageRepo.Update(ds);
            }
            else if (TempWarehouseRequestDTO.RequestType == WarehouseRequestTypeEnum.Export)
            {
                var ds = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(createItem.DeliveryStageId);
                ds.DeliveryStatus = DeliveryStageStatusEnum.TempWarehouseExportPending;
                foreach (var purchaseMaterial in ds.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                {
                    purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.TempWarehouseExportPending;
                }
                _unitOfWork.DeliveryStageRepo.Update(ds);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateApproveStatus(TempWarehouseApproveRequestVM tempWarehouseRequestDTO)
        {
            var approveItem = await _unitOfWork.TempWarehouseRequestRepo.GetByIdWithDSAsync(tempWarehouseRequestDTO.Id);

            if (approveItem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (approveItem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }

            if (tempWarehouseRequestDTO.ApproveExecutionDate.Date < approveItem.RequestExecutionDate.Value.Date)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.INVALID_INFORMATION), ExceptionMessage.INVALID_INFORMATION + " - Execution Date Must Be After Today");
            }
            _mapper.Map(tempWarehouseRequestDTO, approveItem);

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
            // Update status after approve
            if (approveItem.ApproveStatus == ApproveEnum.Approved)
            {
                approveItem.RejectReason = null;

                approveItem.RequestExecutionDate = tempWarehouseRequestDTO.ApproveExecutionDate;
                if (approveItem.RequestType == WarehouseRequestTypeEnum.Import)
                {
                    var ds = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(approveItem.DeliveryStageId);
                    ds.DeliveryStatus = DeliveryStageStatusEnum.TempWarehouseImportAprroved;
                    foreach (var purchaseMaterial in ds.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                    {
                        purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.TempWarehouseImportAprroved;
                    }
                    _unitOfWork.DeliveryStageRepo.Update(ds);
                }
                else if (approveItem.RequestType == WarehouseRequestTypeEnum.Export)
                {
                    var ds = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(approveItem.DeliveryStageId);
                    ds.DeliveryStatus = DeliveryStageStatusEnum.TempWarehouseExportAprroved;
                    foreach (var purchaseMaterial in ds.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                    {
                        purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.TempWarehouseExportAprroved;
                    }
                    _unitOfWork.DeliveryStageRepo.Update(ds);
                }
            }
            else if (approveItem.ApproveStatus == ApproveEnum.Rejected)
            {
                if (approveItem.RequestType == WarehouseRequestTypeEnum.Import)
                {
                    var ds = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(approveItem.DeliveryStageId);
                    ds.DeliveryStatus = DeliveryStageStatusEnum.Delivered;
                    foreach (var purchaseMaterial in ds.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                    {
                        purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.Delivered;
                    }
                    _unitOfWork.DeliveryStageRepo.Update(ds);
                }
                else if (approveItem.RequestType == WarehouseRequestTypeEnum.Export)
                {
                    var ds = await _unitOfWork.DeliveryStageRepo.GetByIdWithDetailAsync(approveItem.DeliveryStageId);
                    ds.DeliveryStatus = DeliveryStageStatusEnum.Inspected;
                    foreach (var purchaseMaterial in ds.PurchaseMaterials.Where(x => x.WarehouseStatus != DeliveryStageStatusEnum.SupInactive))
                    {
                        purchaseMaterial.WarehouseStatus = DeliveryStageStatusEnum.Inspected;
                    }
                    _unitOfWork.DeliveryStageRepo.Update(ds);
                }
            }
            _unitOfWork.TempWarehouseRequestRepo.Update(approveItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(TempWarehouseRequestUpdateVM TempWarehouseRequestDTO)
        {
            var updateItem = await _unitOfWork.TempWarehouseRequestRepo.GetByIdAsync(TempWarehouseRequestDTO.Id);
            if (updateItem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.DELIVERYSTAGE_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (updateItem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }
            _mapper.Map(TempWarehouseRequestDTO, updateItem);
            _unitOfWork.TempWarehouseRequestRepo.Update(updateItem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.REQUEST_NOTEXIST), ExceptionMessage.REQUEST_NOTEXIST);
        }

        public async Task DeleteAsync(int id)
        {
            var deleteitem = await _unitOfWork.TempWarehouseRequestRepo.GetByIdAsync(id);
            if (deleteitem == null)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUEST_NOTEXIST), ExceptionMessage.DELIVERYSTAGE_NOTEXIST);
            }
            else if (deleteitem.ApproveStatus != ApproveEnum.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE), ExceptionMessage.REQUESTSTATUS_NOTAVAILABLE);
            }

            _unitOfWork.TempWarehouseRequestRepo.SoftRemove(deleteitem);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);

        }

        public async Task<List<TempWarehouseRequestVM>> GetAllAsync()
        {
            var items = await _unitOfWork.TempWarehouseRequestRepo.GetAllWithDetailAsync();
            var result = _mapper.Map<List<TempWarehouseRequestVM>>(items);
            return result;
        }

        public async Task<TempWarehouseRequestVM> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.TempWarehouseRequestRepo.GetByIdWithFormAsync(id);

            if (item == null)
            {
                throw new APIException(HttpStatusCode.NotFound,
                    nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<TempWarehouseRequestVM>(item);
            result.DeliveryStage.PurchaseMaterials.RemoveAll(x => x.TotalQuantity <= 0);
            return result;
        }

        public async Task<List<TempWarehouseRequestVM>> GetAllByDeliveryStageIdAsync(int deliveryStageId)
        {
            var itemList = await _unitOfWork.TempWarehouseRequestRepo.GetAllByDeliveryStageIdAsync(deliveryStageId);
            var result = _mapper.Map<List<TempWarehouseRequestVM>>(itemList);

            return result;
        }
    }
}
