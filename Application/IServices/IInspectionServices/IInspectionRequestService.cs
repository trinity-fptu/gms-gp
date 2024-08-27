
using Application.ViewModels.InspectionRequest;
using Application.ViewModels.TempWarehouse;

namespace Application.IServices.IInspectionServices
{
    public interface IInspectionRequestService
    {
        Task<InspectionRequestVM> GetByIdAsync(int id);
        Task<List<InspectionRequestVM>> GetAllAsync();
        Task<List<InspectionRequestVM>> GetAllByDeliveryStageIdAsync(int deliveryStageId);
        Task CreateAsync(InspectionRequestAddVM inspectionRequestVM);
        Task UpdateApproveStatus(InspectionApproveRequestVM approveRequestVM);
        Task UpdateAsync(InspectionRequestUpdateVM requestDTO);
        Task DeleteAsync(int id);
    }
}
