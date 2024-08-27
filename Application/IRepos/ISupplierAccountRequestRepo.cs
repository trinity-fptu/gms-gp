using Domain.Entities.UserRole;

namespace Application.IRepos
{
    public interface ISupplierAccountRequestRepo : IGenericRepo<SupplierAccountRequest>
    {
        Task<List<SupplierAccountRequest>> GetSupplierAccountRequestByPurchasingStaffId(int purchasingStaffId);
    }
}
