using Application.IRepos;
using Application.ViewModels.DeliveryStage;
using Domain.Entities;
using Domain.Enums.DeliveryStage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class PurchaseMaterialRepo : GenericRepo<PurchaseMaterial>, IPurchaseMaterialDetailRepo
    {
        public PurchaseMaterialRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<PurchaseMaterial> GetPurchaseMaterialByDeliveryStageIdAndRawMaterialId(int deliveryStageId, int rawMaterialId)
        {
            var item = await _dbSet
                .FirstOrDefaultAsync(x =>
                    x.RawMaterialId == rawMaterialId
                    && x.DeliveryStageId == deliveryStageId
                    && !x.IsDeleted);

            return item;
        }

        public async Task<List<PurchaseMaterial>> GetPurchaseMaterialListInMainWarehouse(int rawMaterialId)
        {
            var item = await _dbSet
                .Include(x => x.RawMaterial)
                .Include(x => x.DeliveryStage)
                .ThenInclude(x => x.PurchasingOrder)
                .Where(x =>
                    x.RawMaterialId == rawMaterialId
                    && x.WarehouseStatus == DeliveryStageStatusEnum.MainWarehouseImported
                    && !x.IsDeleted).ToListAsync();

            return item;
        }

        public async Task<List<PurchaseMaterial>> GetPurchaseMaterialListInTempWarehouse(int rawMaterialId)
        {
            var item = await _dbSet
                .Include(x => x.RawMaterial)
                .Include(x => x.DeliveryStage)
                .ThenInclude(x => x.PurchasingOrder)
                .Where(x =>
                    x.RawMaterialId == rawMaterialId
                    && 
                        (
                            // check if material is in temp warehouse or inspected
                            (x.WarehouseStatus == DeliveryStageStatusEnum.TempWarehouseImported
                            || x.WarehouseStatus == DeliveryStageStatusEnum.InspectionRequestAprroved
                            || x.WarehouseStatus == DeliveryStageStatusEnum.PendingForInspection
                            || x.WarehouseStatus == DeliveryStageStatusEnum.Inspected)
                            // or the material is in exported from temp warehouse but has return quantity
                        ||
                            (x.WarehouseStatus == DeliveryStageStatusEnum.TempWarehouseExported && x.ReturnQuantity > 0)
                            // or the material is in main warehouse and has return quantity
                        ||
                            (x.WarehouseStatus == DeliveryStageStatusEnum.MainWarehouseImported && x.ReturnQuantity > 0)
                        )
                    && !x.IsDeleted
                ).ToListAsync();

            return item;
        }
    }
}
