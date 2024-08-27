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
    public class InspectionFormRepo : GenericRepo<InspectionForm>, IInspectionFormRepo
    {
        public InspectionFormRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<InspectionForm>> GetAllWithDetailAsync()
        {
            var item = await _dbSet
                .Include(x => x.InspectionRequest)
                .Include(x => x.MaterialInspectResults.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted).ToListAsync();

            return item;
        }

        public async Task<InspectionForm> GetByIdWithDetailAsync(int id)
        {
            var item = await _dbSet
                .Include(x => x.InspectionRequest)
                .Include(x => x.MaterialInspectResults.Where(x => !x.IsDeleted))
                .ThenInclude(x => x.PurchaseMaterial)
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            return item;
        }
    }
}
