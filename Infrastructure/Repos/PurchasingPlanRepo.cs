using Application.IRepos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class PurchasingPlanRepo : GenericRepo<PurchasingPlan>, IPurchasingPlanRepo
    {
        public PurchasingPlanRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<PurchasingPlan> GetByIdWithDetailAsync(int id)
        {
            return await _dbSet.Include(x => x.ProductionPlan)
                .Include(x => x.PurchaseTasks.Where(x => x.IsDeleted == false))
                .Include(x => x.PurchasingOrders.Where(x => x.IsDeleted == false))
                .FirstOrDefaultAsync(x => 
                    x.Id == id 
                    && x.IsDeleted == false);
        }
        public async Task<List<PurchasingPlan>> GetAllWithDetailAsync()
        {
            return await _dbSet.Include(x => x.ProductionPlan)
                .Include(x => x.PurchaseTasks.Where(x => x.IsDeleted == false))
                .Include(x => x.PurchasingOrders.Where(x => x.IsDeleted == false))
                .Where(x => x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<PurchasingPlan>> GetAllByIdPmanagerId(int id)
        {
            return await _dbSet.Include(x => x.ProductionPlan)
                .Include(x => x.PurchaseTasks.Where(x => x.IsDeleted == false))
                .Include(x => x.PurchasingOrders.Where(x => x.IsDeleted == false))
                .Where(x => x.IsDeleted == false && x.PurchasingManagerId == id)              
                .ToListAsync();
        }
        public async Task<List<PurchasingPlan>> GetAllApprovedWithDetailAsync()
        {
            return await _dbSet.Include(x => x.ProductionPlan)
                .Include(x => x.PurchaseTasks.Where(x => x.IsDeleted == false))
                .Include(x => x.PurchasingOrders.Where(x => x.IsDeleted == false))
                .Where(x => x.IsDeleted == false)
                .Where(x => x.ApproveStatus == ApproveEnum.Approved)
                .ToListAsync();
        }
    }
}
