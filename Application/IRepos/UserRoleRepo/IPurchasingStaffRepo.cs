
using Domain.Entities.UserRole;


namespace Application.IRepos.UserRoleRepo
{
    public interface IPurchasingStaffRepo : IGenericRepo<PurchasingStaff>
    {
        Task<PurchasingStaff> GetPurchasingStaffByIdAsync(int id);
    }
}
