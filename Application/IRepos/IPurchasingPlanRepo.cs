using Domain.Entities;

namespace Application.IRepos
{
    public interface IPurchasingPlanRepo : IGenericRepo<PurchasingPlan>
    {
        Task<PurchasingPlan> GetByIdWithDetailAsync(int id);
        Task<List<PurchasingPlan>> GetAllWithDetailAsync();
        Task<List<PurchasingPlan>> GetAllByIdPmanagerId(int id);
        Task<List<PurchasingPlan>> GetAllApprovedWithDetailAsync();
    }
}
