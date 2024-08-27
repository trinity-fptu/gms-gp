

using Application.ViewModels.PurchasingOrder;
using Domain.Enums;

namespace Application.IServices
{
    public interface IPurchasingOrderService
    {
        Task CreateAsync(PurchasingOrderAddVM purchaseOrderAddVM);
        Task<List<PurchasingOrderVM>> GetAllAsync();
        Task<PurchasingOrderVM> GetByIdAsync(int id);
        Task<List<PurchasingOrderVM>> GetAllByPurchasingPlanIdAsync(int purchasingPlanId);
        Task UpdateAsync(PurchasingOrderUpdateVM updateItem);
        Task DeleteAsync(int id);
        Task ApproveAsync(ApproveEnum approvingStatus, int id, bool isSupplier);
        Task<PurchasingOrderVM> GetByPOCodeAsync(string code);
        Task<List<PurchasingOrderVM>> GetAllByPurchasingTaskIdAsync(int purchasingTaskId);
    }
}
