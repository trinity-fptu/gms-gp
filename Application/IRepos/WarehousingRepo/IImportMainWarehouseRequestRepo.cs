using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;

namespace Application.IRepos.WarehousingRepo
{
    public interface IImportMainWarehouseRequestRepo : IGenericRepo<ImportMainWarehouseRequest>
    {
        Task<List<ImportMainWarehouseRequest>> GetAllByDeliveryStageIdAsync(int deliveryStageId);
        Task<ImportMainWarehouseRequest> GetByIdWithFormAsync(int id);
        Task<List<ImportMainWarehouseRequest>> GetAllWithDetailAsync();
    }
}
