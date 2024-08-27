using Application.ViewModels.PO_Report;
using Domain.Enums;

namespace Application.IServices
{
    public interface IPO_ReportService
    {
        Task CreateAsync(PO_ReportAddVM pO_ReportAddVM);
        Task<PO_ReportVM> GetByIdAsync(int id);
        Task<List<PO_ReportVM>> GetAllAsync();
        Task UpdateForSupplierAsync(PO_ReportSupplierUpdateVM pO_ReportUpdateVM);
        Task UpdateForPurchasingStaffAsync(PO_ReportPurchasingStaffUpdateVM pO_ReportUpdateVM);
        Task DeleteAsync(int id);
        Task ChangeResolveStatus(int id, ApproveEnum resolveStatus);
        Task<List<PO_ReportVM>> GetPOReportByPOId(int purchasingOrderId);
        Task<List<PO_ReportVM>> GetPOReportBySupplierId(int supplierId);
        Task<List<PO_ReportVM>> GetPOReportByStaffId(int staffId);
    }
}
