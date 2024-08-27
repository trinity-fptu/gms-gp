using Application.ViewModels.ProductionPlan;
using Microsoft.AspNetCore.Http;

namespace Application.IServices
{
    public interface IProductionPlanService
    {
        Task<ProductionPlanVM> GetByIdAsync(int id);
        Task<List<ProductionPlanVM>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<List<ProductionPlanVM>> GetAllPlanWithoutPurchasingPlanAsync();
        Task<ProductionPlanVM> ImportProductionPlanFile(IFormFile formFile);
        Task CreateAsync(ProductionPlanAddVM productionPlanAddVM);

    }
}
