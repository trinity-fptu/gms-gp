using Domain.Entities;

namespace Application.IRepos
{
    public interface IProductionPlanRepo : IGenericRepo<ProductionPlan>
    {
        Task<ProductionPlan> GetByIdWithDetailsAsync(int id);
        Task<List<ProductionPlan>> GetAllWithoutPurchasingPlanAsync();
        Task<List<ProductionPlan>> GetAllWithDetailsAsync();
    }
}
