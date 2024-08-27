

using Application.ViewModels.MainWarehouse;

namespace Application.IServices.WarehousingServices
{
    public interface IImportMainWarehouseRequestService
    {
        Task<List<ImportMainWarehouseRequestVM>> GetAllByDeliveryStageIdAsync(int deliveryStageId);

        Task CreateAsync(ImportMainWarehouseRequestAddVM mainWarehouseRequestDTO);
        Task UpdateApproveStatus(ImportMainWarehouseApproveRequestVM ApproveRequestDTO);
        Task<ImportMainWarehouseRequestVM> GetByIdAsync(int id);
        Task<List<ImportMainWarehouseRequestVM>> GetAllAsync();
        Task UpdateAsync(ImportMainWarehouseRequestUpdateVM TempWarehouseRequestDTO);
        Task DeleteAsync(int id);
    }
}
