using Domain.Entities;

namespace Application.IRepos
{
    public interface IPurchasingTaskRepo : IGenericRepo<PurchasingTask>
    {
        Task<PurchasingTask> GetByIdWithDetailAsync(int id);
        Task<List<PurchasingTask>> GetPurchasingTaskByPurchasingPlanId(int purchasingPlanId);
        Task<List<PurchasingTask>> GetPurchasingTaskByPurchasingStaffId(int purchasingStaffId);
    }
}
