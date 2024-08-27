using Domain.Entities;
using Domain.Enums.Warehousing;

namespace Application.IRepos
{
    public interface IWarehouseRepo : IGenericRepo<Warehouse>
    {
        Task<List<Warehouse>> GetAllWithWarehouseMaterialAsync();
        Task<Warehouse> GetByIdWithWarehouseMaterialsAsync(int id);
        Task<Warehouse> GetByTypeAsync(WarehouseTypeEnum type);
    }
}
