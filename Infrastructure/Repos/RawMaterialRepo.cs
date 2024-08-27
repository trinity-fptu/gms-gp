using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class RawMaterialRepo : GenericRepo<RawMaterial>, IRawMaterialRepo
    {
        private readonly AppDbContext _context;
        public RawMaterialRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<RawMaterial>> GetRawMaterialByMaterialCategoryId(int materialCategoryId)
        {
            return await _context.RawMaterials
                .Where(x => 
                    x.MaterialCategoryId == materialCategoryId 
                    && x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<RawMaterial> GetByMaterialCode(string code)
        {
            return await _context.RawMaterials
                .FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
