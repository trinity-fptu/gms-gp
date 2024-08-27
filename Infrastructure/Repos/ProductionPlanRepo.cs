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
    public class ProductionPlanRepo : GenericRepo<ProductionPlan>, IProductionPlanRepo
    {
        public ProductionPlanRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<ProductionPlan> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(x => x.ProductInPlans.Where(x => !x.IsDeleted))
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductMaterials)
                .Include(x => x.ExpectedMaterials.Where(x => !x.IsDeleted))
                .Include(x => x.PurchasingPlans)
                .FirstOrDefaultAsync(x =>
                    x.Id == id
                    && !x.IsDeleted);
        }

        public async Task<List<ProductionPlan>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(x => x.ProductInPlans.Where(x => !x.IsDeleted))
                .Include(x => x.ExpectedMaterials.Where(x => !x.IsDeleted))
                .Include(x => x.PurchasingPlans)
                .Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<List<ProductionPlan>> GetAllWithoutPurchasingPlanAsync()
        {
            var items = await _dbSet
                .Include(x => x.ProductInPlans.Where(x => !x.IsDeleted))
                    .ThenInclude(x => x.Product)
                .Include(x => x.ExpectedMaterials.Where(x => !x.IsDeleted))
                .Include(x => x.PurchasingPlans.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted
                && (x.PlanStartDate > DateTime.Now.AddDays(31)))
                .ToListAsync();

            return items;
        }
    }
}
