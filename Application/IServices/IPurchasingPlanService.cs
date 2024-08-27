
using Application.ViewModels.PurchasingPlan;
using Application.ViewModels.RawMaterial;
using Domain.Enums;

namespace Application.IServices
{
    public interface IPurchasingPlanService
    {
        public Task CreateAsync(PurchasingPlanAddVM model);
        Task<PurchasingPlanVM> GetByIdAsync(int id);
        Task<List<PurchasingPlanVM>> GetAllAuthorizeAsync();
        Task UpdateAsync(PurchasingPlanUpdateVM model);
        Task ApproveAsync(ApproveEnum approvingStatus, int id);
        Task DeleteAsync(int id);
        Task<List<PurchasingPlanVM>> GetAllApprovedAsync();
    }
}
