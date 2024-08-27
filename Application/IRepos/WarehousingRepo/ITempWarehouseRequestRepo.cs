using Domain.Entities.Warehousing;


namespace Application.IRepos.WarehousingRepo
{
    public interface ITempWarehouseRequestRepo : IGenericRepo<TempWarehouseRequest>
    {
        Task<List<TempWarehouseRequest>> GetAllByDeliveryStageIdAsync(int deliveryStageId);
        Task<TempWarehouseRequest> GetByIdWithFormAsync(int id);
        Task<TempWarehouseRequest> GetByIdWithDSAsync(int id);
        Task<List<TempWarehouseRequest>> GetAllWithDetailAsync();
    }
}
