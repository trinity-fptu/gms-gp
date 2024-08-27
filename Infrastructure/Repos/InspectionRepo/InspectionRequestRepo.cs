using Application.IRepos.IInspectionRepo;
using Domain.Entities.Inspect;
using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.InspectionRepo
{
    public class InspectionRequestRepo : GenericRepo<InspectionRequest>, IInspectionRequestRepo
    {
        public InspectionRequestRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<InspectionRequest>> GetAllByDeliveryStageIdAsync(int deliveryStageId)
        {
            var itemList = await _dbSet.Where(x => 
                    x.DeliveryStageId == deliveryStageId 
                    && x.IsDeleted == false).Include(x => x.InspectionForm).ThenInclude(x => x.MaterialInspectResults)
                .ToListAsync();
            return itemList;
        }


        public async Task<List<InspectionRequest>> GetAllWithDetailAsync()
        {
            var itemList = await _dbSet
                    .Include(x => x.DeliveryStage)
                    .Include(x => x.InspectionForm)
                .ToListAsync();
            return itemList;
        }

        public async Task<InspectionRequest> GetByIdWithInspectionFormAsync(int id)
        {
            var item = await _dbSet
                .Include(x => x.DeliveryStage)
                    .ThenInclude(x => x.PurchaseMaterials.Where(x => !x.IsDeleted))                    
                .Include(x => x.InspectionForm)
                    .ThenInclude(x => x.MaterialInspectResults.Where(x => !x.IsDeleted))
                    .ThenInclude(x => x.PurchaseMaterial)
                .FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }
    }
}
