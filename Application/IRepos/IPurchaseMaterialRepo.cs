using Domain.Entities;

namespace Application.IRepos
{
    public interface IPurchaseMaterialDetailRepo : IGenericRepo<PurchaseMaterial>
    {
        Task<PurchaseMaterial> GetPurchaseMaterialByDeliveryStageIdAndRawMaterialId(int deliveryStageId, int rawMaterialId);
        Task<List<PurchaseMaterial>> GetPurchaseMaterialListInTempWarehouse(int rawMaterialId);
        Task<List<PurchaseMaterial>> GetPurchaseMaterialListInMainWarehouse(int rawMaterialId);
    }
}
