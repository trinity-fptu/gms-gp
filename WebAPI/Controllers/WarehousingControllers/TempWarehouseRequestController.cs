using Application.IServices.WarehousingServices;
using Application.Services.WarehousingServices;
using Application.ViewModels.TempWarehouse;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;
using Application.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers.WarehousingControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TempWarehouseRequestController : ControllerBase
    {
        private readonly ITempWarehouseRequestService _tempWarehouseRequestService;

        public TempWarehouseRequestController(ITempWarehouseRequestService tempWarehouseRequestService)
        {
            _tempWarehouseRequestService = tempWarehouseRequestService;
        }

        /// <summary>
        /// Get the list of TempWarehouseRequest by deliveryStage id
        /// </summary>
        [HttpGet("deliveryStage/{deliveryStageId}")]
        public async Task<IActionResult> GetAllByPOId(int deliveryStageId)
        {
            var result = await _tempWarehouseRequestService.GetAllByDeliveryStageIdAsync(deliveryStageId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TempWarehouseRequestAddVM tempWarehouseRequestAddVM)
        {
            await _tempWarehouseRequestService.CreateAsync(tempWarehouseRequestAddVM);
            return Ok("Temporary warehouse request create successfully");
        }

        [HttpPut("approval")]
        public async Task<IActionResult> UpdateApproveStatus(TempWarehouseApproveRequestVM tempWarehouseApproveRequestVM)
        {
            await _tempWarehouseRequestService.UpdateApproveStatus(tempWarehouseApproveRequestVM);
            return Ok("Temporary warehouse request approve status updated successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(TempWarehouseRequestUpdateVM tempWarehouseRequestUpdateVM)
        {
            await _tempWarehouseRequestService.UpdateAsync(tempWarehouseRequestUpdateVM);
            return Ok("Temporary warehouse request updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tempWarehouseRequestService.DeleteAsync(id);
            return Ok("Temporary warehouse request deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tempWarehouseRequestService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tempWarehouseRequestService.GetAllAsync();
            return Ok(result);
        }
    }
}
