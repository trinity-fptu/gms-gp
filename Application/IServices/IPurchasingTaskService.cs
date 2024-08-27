
using Application.ViewModels.PurchasingTask;

namespace Application.IServices
{
    public interface IPurchasingTaskService
    {
        Task<PurchasingTaskVM> GetByIdAsync(int id);
        Task<List<PurchasingTaskVM>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<List<PurchasingTaskVM>> GetPurchasingTaskByPurchasingPlanId(int purchasingPlanId);
        Task<List<PurchasingTaskVM>> GetPurchasingTaskByPurchasingStaffId(int purchasingStaffId);
        Task AssignAsync(PurchasingTaskAssignVM model);
    }
}
