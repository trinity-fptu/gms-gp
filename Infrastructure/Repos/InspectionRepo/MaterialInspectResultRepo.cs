using Application.IRepos.IInspectionRepo;
using Domain.Entities.Inspection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.InspectionRepo
{
    public class MaterialInspectResultRepo : GenericRepo<MaterialInspectResult>, IMaterialInspectResultRepo
    {
        public MaterialInspectResultRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<MaterialInspectResult>> GetAllByInspectionFormIdAsync(int inspectionFormId)
        {
            var itemList = await _dbSet
                .Where(x => 
                    x.InspectionFormId == inspectionFormId 
                    && x.IsDeleted == false)
                .ToListAsync();
            return itemList;
        }

        public async Task<MaterialInspectResult> GetByPmId(int id)
        {
            var itemList = await _dbSet
                .FirstOrDefaultAsync(x =>
                    x.PurchaseMaterialId == id
                    && x.IsDeleted == false);
            return itemList;
        }

    }
}
