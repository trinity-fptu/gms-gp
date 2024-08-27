
using Domain.Entities.UserRole;

namespace Application.IRepos.UserRoleRepo
{
    public interface IPurchasingManagerRepo : IGenericRepo<PurchasingManager>
    {
        Task<PurchasingManager> GetPurchasingManagerByIdAsync(int id);
    }
}
