
using Domain.Entities.Warehousing;

namespace Application.IRepos.WarehousingRepo
{
    public interface IWarehouseFormRepo : IGenericRepo<WarehouseForm>
    {
        Task<WarehouseForm> GetByIdWithMaterialsAsync(int id);
        Task<WarehouseForm> GetImportMainFormByDeliveryStageIdWithMaterialsAsync(int id);
        Task<WarehouseForm> GetExportFormByDeliveryStageIdWithMaterialsAsync(int id);
    }
}
