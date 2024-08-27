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
    public class WarehouseMaterialRepo : GenericRepo<WarehouseMaterial>, IWarehouseMaterialRepo
    {
        private readonly AppDbContext _context;

        public WarehouseMaterialRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<WarehouseMaterial>> GetAllWithRawMaterialsAsync()
        {
            return await _context.WarehouseMaterials
                .Where(x => x.IsDeleted == false)
                .Include(x => x.RawMaterial)
                .ToListAsync();
        }

        public async Task<WarehouseMaterial> GetByIdWithRawMaterialAsync(int id)
        {
            return await _context.WarehouseMaterials
                .Where(x => x.IsDeleted == false)
                .Include(wm => wm.RawMaterial)
                .FirstOrDefaultAsync(wm => wm.Id == id);
        }

        public async Task<List<WarehouseMaterial>> GetWarehouseMaterialsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.WarehouseMaterials
                .Where(x => x.IsDeleted == false)
                .Where(wm => wm.WarehouseId == warehouseId)
                .Include(wm => wm.RawMaterial)
                .ToListAsync();
        }

        public async Task<WarehouseMaterial> GetTempMaterial(int id)
        {
            return await _context.WarehouseMaterials.Include(x => x.Warehouse)
                .FirstOrDefaultAsync(x => x.RawMaterialId == id && x.Warehouse.WarehouseType == Domain.Enums.Warehousing.WarehouseTypeEnum.TempWarehouse);
        }
        
        public async Task<WarehouseMaterial> GetMainMaterial(int id)
        {
            return await _context.WarehouseMaterials.Include(x => x.Warehouse)
                .FirstOrDefaultAsync(x => x.RawMaterialId == id && x.Warehouse.WarehouseType == Domain.Enums.Warehousing.WarehouseTypeEnum.MainWarehouse);
        }
    }
}
