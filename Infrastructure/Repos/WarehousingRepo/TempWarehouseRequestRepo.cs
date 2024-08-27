using Application.IRepos.WarehousingRepo;
using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.WarehousingRepo
{
    public class TempWarehouseRequestRepo : GenericRepo<TempWarehouseRequest>, ITempWarehouseRequestRepo
    {
        public TempWarehouseRequestRepo(AppDbContext context) : base(context)
        {

        }

        public async Task<List<TempWarehouseRequest>> GetAllByDeliveryStageIdAsync(int deliveryStageId)
        {
            var itemList = await _dbSet.Where(x =>
                    x.DeliveryStageId == deliveryStageId
                    && x.IsDeleted == false).Include(x => x.WarehouseForm).ThenInclude(x => x.WarehouseFormMaterials)
                .ToListAsync();
            return itemList;
        }

        public async Task<List<TempWarehouseRequest>> GetAllWithDetailAsync()
        {
            var itemList = await _dbSet
                    .Include(x => x.DeliveryStage)
                    .Include(x => x.WarehouseForm)
                .ToListAsync();
            return itemList;
        }

        public async Task<TempWarehouseRequest> GetByIdWithFormAsync(int id)
        {
            return await _dbSet
                .Include(x => x.DeliveryStage)
                    .ThenInclude(x => x.PurchaseMaterials)
                .Include(x => x.WarehouseForm)
                    .ThenInclude(x => x.WarehouseFormMaterials.Where(x => !x.IsDeleted))
                    .ThenInclude(x => x.PurchaseMaterial)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
        public async Task<TempWarehouseRequest> GetByIdWithDSAsync(int id)
        {
            return await _dbSet
                .Include(x => x.DeliveryStage)
                .ThenInclude(x => x.PurchaseMaterials.Where(x => !x.IsDeleted))
                .Include(x => x.WarehouseForm)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
