using Domain.Entities;

namespace Application.IRepos
{
    public interface IWarehouseMaterialRepo : IGenericRepo<WarehouseMaterial>
    {
        Task<List<WarehouseMaterial>> GetAllWithRawMaterialsAsync();
        Task<WarehouseMaterial> GetByIdWithRawMaterialAsync(int id);
        Task<List<WarehouseMaterial>> GetWarehouseMaterialsByWarehouseIdAsync(int warehouseId);
        Task<WarehouseMaterial> GetMainMaterial(int id);
        Task<WarehouseMaterial> GetTempMaterial(int id);
    }
}
