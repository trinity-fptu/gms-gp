using Application.IRepos;
using Application.ViewModels.PurchasingOrder;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class DeliveryStageRepo : GenericRepo<DeliveryStage>, IDeliveryStageRepo
    {
        public DeliveryStageRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<DeliveryStage>> GetAllByPurchasingOrderIdAsync(int purchasingOrderId)
        {
            return await _dbSet
                .Include(x => x.PurchaseMaterials)
                .Where(x => x.PurchasingOrderId == purchasingOrderId && x.IsDeleted == false)
                .ToListAsync();
        }
        public async Task<List<DeliveryStage>> GetAllWithDetailAsync()
        {
            return await _dbSet
                .Include(x => x.PurchasingOrder)
                .Include(x => x.PurchaseMaterials)
                .Where(x => x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<DeliveryStage> GetByIdWithPO(int id)
        {
            var item = _dbSet
                .Include(x => x.PurchaseMaterials)
                .Include(x => x.PurchasingOrder)
                .FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return item;
        }
        public async Task<DeliveryStage> GetByIdWithDetailAsync(int id)
        {
            var item = _dbSet
                .Include(x => x.PurchaseMaterials.Where(x => x.IsDeleted == false))
                .FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return item;
        }
    }
}
