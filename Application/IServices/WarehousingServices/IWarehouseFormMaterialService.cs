

using Application.ViewModels.WarehouseFormMaterial;
using Domain.Enums.Warehousing;

namespace Application.IServices.WarehousingServices
{
    public interface IWarehouseFormMaterialService
    {
        Task<List<WarehouseFormMaterialVM>> GetAllByWarehouseFormIdAsync(int warehouseFormId);
        Task UpdateStatus(int warehouseFormMaterialId, WarehouseFormStatusEnum status);
    }
}
