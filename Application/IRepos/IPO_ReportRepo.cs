using Domain.Entities;

namespace Application.IRepos
{
    public interface IPO_ReportRepo : IGenericRepo<PO_Report>
    {
        Task<List<PO_Report>> GetPOReportByPOId(int purchasingOrderId);
        Task<List<PO_Report>> GetPOReportBySupplierId(int supplierId);
        Task<List<PO_Report>> GetPOReportByStaffId(int staffId);
    }
}
