using Application.IServices;
using Application.Services;
using Application.ViewModels.DeliveryStage;
using Domain.Enums.DeliveryStage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryStageController : ControllerBase
    {
        private readonly IDeliveryStageService _deliveryStageService;

        public DeliveryStageController(IDeliveryStageService deliveryStageService)
        {
            _deliveryStageService = deliveryStageService;
        }

        /// <summary>
        /// Get the list of all delivery stage
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _deliveryStageService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get the list of all delivery stage by purchasing order id
        /// </summary>
        [HttpGet("purchasing-order/{purchasingOrderId}")]
        public async Task<IActionResult> GetAllByPOId (int purchasingOrderId)
        {
            var result = await _deliveryStageService.GetAllByPurchasingOrderIdAsync(purchasingOrderId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _deliveryStageService.GetByIdAsync(id);
            return Ok(result);
        }
        
        [HttpGet("inspected/{deliveryStageId}")]
        public async Task<IActionResult> GetInspectedByIdAsync(int deliveryStageId)
        {
            var result = await _deliveryStageService.GetInspectedDeliveryStageByIdAsync(deliveryStageId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deliveryStageService.DeleteAsync(id);
            return Ok("Delivery stage delete success");
        }

        /// <summary>
        /// Change the delivery stage with the given id to the given status
        /// </summary>
        [HttpPut("{id}/status/{status}")]   
        public async Task<IActionResult> ChangeStageStatus(int id, DeliveryStageStatusEnum status)
        {
            await _deliveryStageService.ChangeDeliveryStageStatusAsync(id, status);
            return Ok("Delivery stage status change success");
        }

        /// <summary>
        /// Change the purchase material quantity for delivery stage with the given id 
        /// </summary>
        [HttpPut("quantity")]
        public async Task<IActionResult> ChangeStageQuantity(DeliveryStageUpdateQuantityVM updateQuantityVM)
        {
            await _deliveryStageService.ChangeDeliveryStageQuantityAsync(updateQuantityVM);
            return Ok("Delivery stage quantity changed success");
        }

        [HttpPut]
        public async Task<IActionResult> Update(DeliveryStageUpdateVM updateItem)
        {
            await _deliveryStageService.UpdateAsync(updateItem);
            return Ok("Delivery stage update success");
        }

        /// <summary>
        /// Update the list of delivery stages with purchase materials for a purchasing order id
        /// </summary>
        [HttpPut("purchasing-order/{purchasingOrderId}")]
        public async Task<IActionResult> UpdateByPurchasingOrderId(List<DeliveryStageUpdateVM> deliveryStageUpdateVM, int purchasingOrderId)
        {
            await _deliveryStageService.UpdateDeliveryStagesAsync(deliveryStageUpdateVM, purchasingOrderId);
            return Ok("Delivery stage for this purchase order update successfully");
        }


        /// <summary>
        /// Create delivery stages with purchase materials for a purchasing order id
        /// </summary>
        [HttpPost("purchasing-order/{purchasingOrderId}")]
        public async Task<IActionResult> CreateByPurchasingOrderId(List<DeliveryStageAddVM> deliveryStageAddVM, int purchasingOrderId)
        {
            await _deliveryStageService.AddDeliveryStages(deliveryStageAddVM, purchasingOrderId);
            return Ok("Delivery stage for this purchase order created successfully");
        }

        /// <summary>
        /// Get the list of all delivery stage by temp warehouse request id
        /// </summary>
        [HttpGet("temp-warehouse-request/{requestId}")]
        public async Task<IActionResult> GetByTempWarehouseRequestId(int requestId)
        {
            var result = await _deliveryStageService.GetByTempWarehouseRequest(requestId);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of all delivery stage by main warehouse request id
        /// </summary>
        [HttpGet("import-main-warehouse-request/{requestId}")]
        public async Task<IActionResult> GetByImportMainWarehouseRequest(int requestId)
        {
            var result = await _deliveryStageService.GetByImportMainWarehouseRequest(requestId);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of all delivery stage by inspection request id
        /// </summary>
        [HttpGet("inspection-request/{requestId}")]
        public async Task<IActionResult> GetByInspectionRequest(int requestId)
        {
            var result = await _deliveryStageService.GetByInspectionRequest(requestId);
            return Ok(result);
        }

        [HttpPost("start-delivering-supplemental")]
        public async Task<IActionResult> PerformDeliveringSupplementalDeliveryStage(DeliveryStageSuppStartDeliveringVM deliveringState)
        {
            await _deliveryStageService.PerformDeliveringSupplementalDeliveryStage(deliveringState);
            return Ok("Delivery stage start delivering supplemental success");
        }

        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelDeliveryStage(int id)
        {
            await _deliveryStageService.CancelDeliveryStage(id);
            return Ok("Cancel delivery stage success");
        }
    }
}
