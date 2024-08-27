

using Application.ViewModels.PurchasingOrder.OrderMaterial;

namespace Application.IServices
{
    public interface IOrderMaterialService
    {
        Task<OrderMaterialVM> GetByIdAsync(int id);
        Task<List<OrderMaterialVM>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<List<OrderMaterialVM>> GetOrderMaterialByPOId(int purchasingOrderId);
    }
}
