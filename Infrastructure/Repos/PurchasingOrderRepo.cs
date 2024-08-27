using Application.IRepos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class PurchasingOrderRepo : GenericRepo<PurchasingOrder>, IPurchasingOrderRepo
    {
        public PurchasingOrderRepo(AppDbContext context) : base(context)
        {
        }


        public async Task<PurchasingOrder> GetByIdWithDetailAsync(int id)
        {
            return await _dbSet
                .Include(x => x.OrderMaterials.Where(x => !x.IsDeleted))
                .Include(x => x.DeliveryStages.Where(x => !x.IsDeleted))
                    .ThenInclude(x => x.PurchaseMaterials.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(x => 
                    x.Id == id 
                    && !x.IsDeleted);
        }

        public async Task<List<PurchasingOrder>> GetAllWithDetailAsync()
        {
            return await _dbSet
                .Include(x => x.OrderMaterials.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<PurchasingOrder>> GetAllPOForAuthorize()
        {
            var Dstage = await _dbSet.Include(x => x.DeliveryStages.Where(x => !x.IsDeleted))
                .ThenInclude(x => x.TempWarehouseRequests.Where(x => !x.IsDeleted))
                .Include(x => x.DeliveryStages.Where(x => !x.IsDeleted))
                .ThenInclude(x => x.ImportMainWarehouseRequests.Where(x => !x.IsDeleted))
                .Include(x => x.DeliveryStages.Where(x => !x.IsDeleted))
                .ThenInclude(x => x.InspectionRequests.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            return Dstage;
        }

        public async Task<PurchasingOrder> GetByPOCodeAsync(string code)
        {
            var item = await _dbSet
                .Include(x => x.OrderMaterials.Where(x => !x.IsDeleted))
                .Include(x => x.DeliveryStages.Where(x => !x.IsDeleted))
                    .ThenInclude(x => x.PurchaseMaterials.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(x => x.POCode == code && !x.IsDeleted);

            return item;
        }
        public async Task<List<PurchasingOrder>> GetBySupplierId(int id)
        {
            var Dstage = await _dbSet.Include(x => x.OrderMaterials.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted && x.SupplierId == id)
                .ToListAsync();
            return Dstage;
        }

        public async Task<List<PurchasingOrder>> GetByPmanagerId(int id)
        {
            var Dstage = await _dbSet.Include(x => x.OrderMaterials.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted && x.PurchasingPlan.PurchasingManagerId == id)
                .ToListAsync();
            return Dstage;
        }

        public async Task<List<PurchasingOrder>> GetByPstaffId(int id)
        {
            var Dstage = await _dbSet.Include(x => x.OrderMaterials.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted && x.PurchasingStaffId == id)
                .ToListAsync();
            return Dstage;
        }

        public async Task<List<PurchasingOrder>> GetAllIncludePurchasingPlan(int id)
        {
            var Dstage = await _dbSet.Include(x => x.PurchasingPlan)
                .Where(x => !x.IsDeleted && x.PurchasingStaffId == id)
                .ToListAsync();
            return Dstage;
        }

        public async Task<List<PurchasingOrder>> GetAllWithDetailByPurchasingPlanIdAsync(int purchasingPlanId)
        {
            return await _dbSet
                .Include(x => x.OrderMaterials.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted && x.PurchasingPlanId == purchasingPlanId)
                .ToListAsync();
        }
    }
}
