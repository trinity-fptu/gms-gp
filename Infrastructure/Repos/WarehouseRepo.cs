using Application.IRepos;
using Domain.Entities;
using Domain.Enums.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class WarehouseRepo : GenericRepo<Warehouse>, IWarehouseRepo
    {
        private readonly AppDbContext _context;

        public WarehouseRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>> GetAllWithWarehouseMaterialAsync()
        {
            return await _context.Warehouses
                .Where(x => x.IsDeleted == false)
                .Include(wm => wm.WarehouseMaterials.Where(x => x.IsDeleted == false))
                    .ThenInclude(wm => wm.RawMaterial)
                .ToListAsync();
        }

        public async Task<Warehouse> GetByIdWithWarehouseMaterialsAsync(int id)
        {
            return await _context.Warehouses
                .Where(x => x.IsDeleted == false)
                .Include(w => w.WarehouseMaterials.Where(x => x.IsDeleted == false))
                    .ThenInclude(wm => wm.RawMaterial)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Warehouse> GetByTypeAsync(WarehouseTypeEnum type)
        {
            return await _context.Warehouses
                .Where(x => x.IsDeleted == false)
                .Include(w => w.WarehouseMaterials.Where(x => x.IsDeleted == false))
                    .ThenInclude(wm => wm.RawMaterial)
                .FirstOrDefaultAsync(w => w.WarehouseType == type);
        }
    }
}
