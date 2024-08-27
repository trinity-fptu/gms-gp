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
    public class PurchasingTaskRepo : GenericRepo<PurchasingTask>, IPurchasingTaskRepo
    {
        private readonly AppDbContext _context;

        public PurchasingTaskRepo(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<PurchasingTask> GetByIdWithDetailAsync(int id)
        {
            var item = await _dbSet.Include(x => x.PurchasingPlan)
                .FirstOrDefaultAsync(x => 
                    x.Id == id 
                    && x.IsDeleted == false);

            return item;
        }

        public async Task<List<PurchasingTask>> GetPurchasingTaskByPurchasingPlanId(int purchasingPlanId)
        {
            return await _context.PurchasingTasks
                .Where(x => 
                    x.PurchasingPlanId == purchasingPlanId 
                    && x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<PurchasingTask>> GetPurchasingTaskByPurchasingStaffId(int purchasingStaffId)
        {
            return await _context.PurchasingTasks
                .Where(x => 
                    x.PurchasingStaffId == purchasingStaffId 
                    && x.IsDeleted == false)
                .ToListAsync();
        }
    }
}
