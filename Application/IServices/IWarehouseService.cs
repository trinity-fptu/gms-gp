
using Application.ViewModels.Warehouse;
using Application.ViewModels.WarehouseMaterial;

namespace Application.IServices
{
    public interface IWarehouseService
    {
        Task CreateAsync(WarehouseAddVM warehouseAddVM);
        Task<WarehouseVM> GetByIdAsync(int id);
        Task<List<WarehouseVM>> GetAllAsync();
        Task UpdateAsync(WarehouseUpdateVM warehouseUpdateVM); 
        Task DeleteAsync(int id);
        Task UpdateMissingWarehouseMaterial();
        Task<List<TempWarehouseMaterialVM>> GetPurchaseMaterialListInTempWarehouse(int warehouseId, int rawMaterialId);
        Task<List<MainWarehouseMaterialVM>> GetPurchaseMaterialListInMainWarehouse(int warehouseId, int rawMaterialId);
    }
}
