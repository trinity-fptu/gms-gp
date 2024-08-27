

using Application.ViewModels.DeliveryStage;
using Domain.Enums.DeliveryStage;

namespace Application.IServices
{
    public interface IDeliveryStageService
    {
        Task<DeliveryStageVM> GetByIdAsync(int id);
        Task<DeliveryStageWithInspectionRequestVM> GetInspectedDeliveryStageByIdAsync(int id);
        Task<List<DeliveryStageVM>> GetAllAsync();
        Task<List<DeliveryStageVM>> GetAllByPurchasingOrderIdAsync(int purchasingOrderId);
        Task DeleteAsync(int id);
        Task ChangeDeliveryStageStatusAsync(int id, DeliveryStageStatusEnum status);
        Task TryChangeDeliveryStageStatusAsync(int id, DeliveryStageStatusEnum status);
        Task UpdateAsync(DeliveryStageUpdateVM updateItem);
        Task AddDeliveryStages(List<DeliveryStageAddVM> addingDeliveryStages, int purchaseOrderId);
        Task UpdateDeliveryStagesAsync(List<DeliveryStageUpdateVM> updatingDeliveryStagesVM, int purchaseOrderId);

        Task<DeliveryStageVM> GetByTempWarehouseRequest(int requestId);
        Task<DeliveryStageVM> GetByImportMainWarehouseRequest(int requestId);
        Task<DeliveryStageVM> GetByInspectionRequest(int requestId);
        Task ChangeDeliveryStageQuantityAsync(DeliveryStageUpdateQuantityVM updateQuantityVM);
        Task PerformDeliveringSupplementalDeliveryStage(DeliveryStageSuppStartDeliveringVM deliveringState);
        Task CancelDeliveryStage(int deliveryStageId);
    }
}
