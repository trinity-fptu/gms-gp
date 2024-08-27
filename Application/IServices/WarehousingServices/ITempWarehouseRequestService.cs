using Application.ViewModels.MaterialCategory;
using Application.ViewModels.TempWarehouse;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices.WarehousingServices
{
    public interface ITempWarehouseRequestService
    {
        Task CreateAsync(TempWarehouseRequestAddVM TempWarehouseRequestDTO);
        Task UpdateApproveStatus(TempWarehouseApproveRequestVM ApproveRequestDTO);
        Task<TempWarehouseRequestVM> GetByIdAsync(int id);
        Task<List<TempWarehouseRequestVM>> GetAllAsync();
        Task<List<TempWarehouseRequestVM>> GetAllByDeliveryStageIdAsync(int deliveryStageId);
        Task UpdateAsync(TempWarehouseRequestUpdateVM TempWarehouseRequestDTO);
        Task DeleteAsync(int id);
    }
}
