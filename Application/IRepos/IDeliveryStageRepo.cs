using Domain.Entities;

namespace Application.IRepos
{
    public interface IDeliveryStageRepo : IGenericRepo<DeliveryStage>
    {
        Task<List<DeliveryStage>> GetAllByPurchasingOrderIdAsync(int purchasingOrderId);
        Task<DeliveryStage> GetByIdWithPO(int id);
        Task<DeliveryStage> GetByIdWithDetailAsync(int id);
        Task<List<DeliveryStage>> GetAllWithDetailAsync();
    }
}
