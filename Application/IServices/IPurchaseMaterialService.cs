using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Domain.Enums.DeliveryStage;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IPurchaseMaterialService
    {
        Task<PurchaseMaterialVM> GetByIdAsync(int id);
        Task<List<PurchaseMaterialVM>> GetAllAsync();
        Task DeleteAsync(int id);
        Task ChangeWarehouseStatusAsync(int id, WarehouseStatusEnum status);
    }
}
