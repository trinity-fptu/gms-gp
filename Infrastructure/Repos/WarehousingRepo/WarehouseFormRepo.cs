using Application.IRepos.WarehousingRepo;
using Application.ViewModels.WarehouseForm;
using Domain.Entities.Warehousing;
using Domain.Enums.Warehousing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.WarehousingRepo
{
    public class WarehouseFormRepo : GenericRepo<WarehouseForm>, IWarehouseFormRepo
    {
        public WarehouseFormRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<WarehouseForm> GetByIdWithMaterialsAsync(int id)
        {
            return await _dbSet
                .Include(x => x.WarehouseFormMaterials.Where(x => !x.IsDeleted))
                .ThenInclude(x => x.PurchaseMaterial)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<WarehouseForm> GetExportFormByDeliveryStageIdWithMaterialsAsync(int id)
        {
            var result = await _dbSet
                .Include(x => x.WarehouseFormMaterials.Where(x => !x.IsDeleted))
                .ThenInclude(x => x.PurchaseMaterial)
                .FirstOrDefaultAsync(x => x.DeliveryStageId == id
                    && !x.IsDeleted
                    && x.FormType == WarehouseFormTypeEnum.Export);

            return result;
        }

        public async Task<WarehouseForm> GetImportMainFormByDeliveryStageIdWithMaterialsAsync(int id)
        {
            var result = await _dbSet
                .Include(x => x.WarehouseFormMaterials.Where(x => !x.IsDeleted))
                .ThenInclude(x => x.PurchaseMaterial)
                .FirstOrDefaultAsync(x => x.DeliveryStageId == id 
                    && !x.IsDeleted 
                    && x.FormType == WarehouseFormTypeEnum.Import
                    && x.ReceiveWarehouse == WarehouseTypeEnum.MainWarehouse);

            return result;
        }
    }
}
