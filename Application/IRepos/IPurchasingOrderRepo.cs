using Domain.Entities;

namespace Application.IRepos
{
    public interface IPurchasingOrderRepo : IGenericRepo<PurchasingOrder>
    {
        Task<List<PurchasingOrder>> GetAllWithDetailAsync();
        Task<PurchasingOrder> GetByIdWithDetailAsync(int id);
        Task<List<PurchasingOrder>> GetAllWithDetailByPurchasingPlanIdAsync(int purchasingPlanId);
        Task<List<PurchasingOrder>> GetAllPOForAuthorize();
        Task<List<PurchasingOrder>> GetBySupplierId(int id);
        Task<List<PurchasingOrder>> GetByPstaffId(int id);
        Task<List<PurchasingOrder>> GetAllIncludePurchasingPlan(int id);
        Task<List<PurchasingOrder>> GetByPmanagerId(int id);
        Task<PurchasingOrder> GetByPOCodeAsync(string code);
    }
}
