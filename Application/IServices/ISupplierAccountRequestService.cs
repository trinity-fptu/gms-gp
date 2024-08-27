
using Application.ViewModels.SupplierAccountRequest;
using Domain.Enums;

namespace Application.IServices
{
    public interface ISupplierAccountRequestService
    {
        Task CreateAsync(SupplierAccountRequestAddVM supplierAccountRequestAddVM);
        Task<SupplierAccountRequestVM> GetByIdAsync(int id);
        Task<List<SupplierAccountRequestVM>> GetAllAsync();
        Task UpdateAsync(SupplierAccountRequestUpdateVM supplierAccountRequestUpdateVM);
        Task DeleteAsync(int id);
        Task ChangeApprovalStatus(int id, ApproveEnum approvalStatus);
        Task<List<SupplierAccountRequestVM>> GetSupplierAccountRequestByPurchasingStaffId(int purchasingStaffId);
    }
}
