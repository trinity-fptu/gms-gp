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
    public class ImportMainWarehouseRequestRepo : GenericRepo<ImportMainWarehouseRequest>, IImportMainWarehouseRequestRepo
    {
        public ImportMainWarehouseRequestRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ImportMainWarehouseRequest>> GetAllByDeliveryStageIdAsync(int deliveryStageId)
        {
            var itemList = await _dbSet.Where(x => 
                    x.DeliveryStageId == deliveryStageId 
                    && x.IsDeleted == false)
                .ToListAsync();
            return itemList;
        }

        public async Task<List<ImportMainWarehouseRequest>> GetAllWithDetailAsync()
        {
            var itemList = await _dbSet
                    .Include(x => x.DeliveryStage)
                    .Include(x => x.WarehouseForm)
                .ToListAsync();
            return itemList;
        }

        public async Task<ImportMainWarehouseRequest> GetByIdWithFormAsync(int id)
        {
            return await _dbSet
                .Include(x=>x.DeliveryStage)
                    .ThenInclude(x=>x.PurchaseMaterials)
                .Include(x => x.WarehouseForm)
                    .ThenInclude(x => x.WarehouseFormMaterials.Where(x => !x.IsDeleted))
                    .ThenInclude(x => x.PurchaseMaterial)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
