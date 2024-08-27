using Application.IRepos.WarehousingRepo;
using Domain.Entities.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.WarehousingRepo
{
    public class WarehouseFormMaterialRepo : GenericRepo<WarehouseFormMaterial>, IWarehouseFormMaterialRepo
    {
        public WarehouseFormMaterialRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<WarehouseFormMaterial>> GetAllByWarehouseFormIdAsync(int warehouseFormId)
        {
            var itemList = await _dbSet
                .Where(x => 
                    x.WarehouseFormId == warehouseFormId 
                    && x.IsDeleted == false)
                .ToListAsync();
            return itemList;
        }
    }
}
