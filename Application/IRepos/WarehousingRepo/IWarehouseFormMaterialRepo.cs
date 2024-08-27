
using Domain.Entities.Warehousing;

namespace Application.IRepos.WarehousingRepo
{
    public interface IWarehouseFormMaterialRepo : IGenericRepo<WarehouseFormMaterial>
    {
        Task<List<WarehouseFormMaterial>> GetAllByWarehouseFormIdAsync(int warehouseFormId);
    }
}
