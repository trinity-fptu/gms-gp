
using Application.ViewModels.WarehouseMaterial;

namespace Application.IServices
{
    public interface IWarehouseMaterialService
    {
        Task CreateAsync(WarehouseMaterialAddVM warehouseMaterialAddVM);
        Task<WarehouseMaterialVM> GetByIdAsync(int id);
        Task<List<WarehouseMaterialVM>> GetAllAsync();
        Task UpdateAsync(WarehouseMaterialUpdateVM warehouseMaterialUpdateVM);
        Task DeleteAsync(int id);
        Task<List<WarehouseMaterialVM>> GetByWarehouseIdAsync(int warehouseId);
    }
}
