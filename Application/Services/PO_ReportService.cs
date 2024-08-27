using Application.Exceptions;
using Application.IServices;
using Application.ViewModels.PO_Report;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Enums.DeliveryStage;
using System.Net;

namespace Application.Services
{
    public class PO_ReportService : IPO_ReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;

        public PO_ReportService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task ChangeResolveStatus(int id, ApproveEnum resolveStatus)
        {
            var existingPO_Report = await _unitOfWork.PO_ReportRepo.GetByIdAsync(id);

            if (existingPO_Report == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + ": PO Report");
            }

            existingPO_Report.SupplierApprovingStatus = resolveStatus;

            _unitOfWork.PO_ReportRepo.Update(existingPO_Report);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.UPDATE_RESOLVE_STATUS), ExceptionMessage.UPDATE_RESOLVE_STATUS);

            if (resolveStatus == ApproveEnum.Approved)
            {
                var purchasingOrder = await _unitOfWork.PurchasingOrderRepo.GetByIdWithDetailAsync(existingPO_Report.PurchasingOrderId.Value);

                if (purchasingOrder != null)
                {
                    purchasingOrder.SupplierApproveStatus = ApproveEnum.Approved;
                    purchasingOrder.DeliveryStages.ToList().ForEach(deliveryStage =>
                    {
                        deliveryStage.DeliveryStatus = DeliveryStageStatusEnum.Approved;
                        deliveryStage.PurchaseMaterials.ToList().ForEach(x => x.WarehouseStatus = DeliveryStageStatusEnum.Approved);
                    });
                    _unitOfWork.PurchasingOrderRepo.Update(purchasingOrder);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task CreateAsync(PO_ReportAddVM pO_ReportAddVM)
        {
            var purchasingOrder = await _unitOfWork.PurchasingOrderRepo.GetByIdAsync(pO_ReportAddVM.PurchasingOrderId);
            if (purchasingOrder == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + ": Purchasing Order");
            }

            var createPO_Report = _mapper.Map<PO_Report>(pO_ReportAddVM);
            createPO_Report.IsDeleted = false;
            createPO_Report.ResolveStatus = PO_ReportEnums.ResolveEnums.Pending;
            createPO_Report.SupplierApprovingStatus = ApproveEnum.Pending;

            await _unitOfWork.PO_ReportRepo.AddAsync(createPO_Report);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_CREATE_ERROR), ExceptionMessage.ENTITY_CREATE_ERROR);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.PO_ReportRepo.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + ": PO Report");
            }
            if (itemToDelete.ResolveStatus != PO_ReportEnums.ResolveEnums.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REPORT_NOT_PENDING), ExceptionMessage.REPORT_NOT_PENDING);
            }

            _unitOfWork.PO_ReportRepo.SoftRemove(itemToDelete);
            if (await _unitOfWork.SaveChangesAsync() == 0) throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_DELETE_ERROR), ExceptionMessage.ENTITY_DELETE_ERROR);
        }
        
        public async Task<List<PO_ReportVM>> GetAllAsync()
        {
            try
            {
                var pO_Reports = await _unitOfWork.PO_ReportRepo.GetAllAsync();
                var result = _mapper.Map<List<PO_ReportVM>>(pO_Reports);
                return result;
            }
            catch (Exception ex)
            {
                throw new APIException(HttpStatusCode.InternalServerError, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
        }

        public async Task<PO_ReportVM> GetByIdAsync(int id)
        {
            var pO_Report = await _unitOfWork.PO_ReportRepo.GetByIdAsync(id);

            if (pO_Report == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);

            }
            var result = _mapper.Map<PO_ReportVM>(pO_Report);
            return result;
        }

        public async Task<List<PO_ReportVM>> GetPOReportByPOId(int purchasingOrderId)
        {
            var poReports = await _unitOfWork.PO_ReportRepo.GetPOReportByPOId(purchasingOrderId);
            if (poReports == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<List<PO_ReportVM>>(poReports);
            return result;
        }

        public async Task<List<PO_ReportVM>> GetPOReportBySupplierId(int supplierId)
        {
            var poReports = await _unitOfWork.PO_ReportRepo.GetPOReportBySupplierId(supplierId);
            if (poReports == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<List<PO_ReportVM>>(poReports);
            return result;
        }

        public async Task<List<PO_ReportVM>> GetPOReportByStaffId(int staffId)
        {
            var poReports = await _unitOfWork.PO_ReportRepo.GetPOReportByStaffId(staffId);
            if (poReports == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND);
            }
            var result = _mapper.Map<List<PO_ReportVM>>(poReports);
            return result;
        }

        public async Task UpdateForPurchasingStaffAsync(PO_ReportPurchasingStaffUpdateVM pO_ReportUpdateVM)
        {
            var existingPO_Report = await _unitOfWork.PO_ReportRepo.GetByIdAsync(pO_ReportUpdateVM.Id);
            if (existingPO_Report == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + ": PO Report");
            }

            _mapper.Map(pO_ReportUpdateVM, existingPO_Report);
            existingPO_Report.ResolveStatus = PO_ReportEnums.ResolveEnums.Resolved;
            _unitOfWork.PO_ReportRepo.Update(existingPO_Report);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }

        public async Task UpdateForSupplierAsync(PO_ReportSupplierUpdateVM pO_ReportUpdateVM)
        {
            var existingPO_Report = await _unitOfWork.PO_ReportRepo.GetByIdAsync(pO_ReportUpdateVM.Id);
            if (existingPO_Report == null)
            {
                throw new APIException(HttpStatusCode.NotFound, nameof(ExceptionMessage.NOT_FOUND), ExceptionMessage.NOT_FOUND + ": PO Report");
            }
            if (existingPO_Report.ResolveStatus != PO_ReportEnums.ResolveEnums.Pending)
            {
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.REPORT_NOT_PENDING), ExceptionMessage.REPORT_NOT_PENDING);
            }

            _mapper.Map(pO_ReportUpdateVM, existingPO_Report);
            _unitOfWork.PO_ReportRepo.Update(existingPO_Report);
            if (await _unitOfWork.SaveChangesAsync() == 0)
                throw new APIException(HttpStatusCode.BadRequest, nameof(ExceptionMessage.ENTITY_UPDATE_ERROR), ExceptionMessage.ENTITY_UPDATE_ERROR);
        }
    }
}
