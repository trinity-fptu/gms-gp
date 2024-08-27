using Application.IServices.WarehousingServices;
using Application.Services.WarehousingServices;
using Application.ViewModels.MainWarehouse;
using Application.ViewModels.TempWarehouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.WarehousingControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImportMainWarehouseRequestController : ControllerBase
    {
        private readonly IImportMainWarehouseRequestService _importMainWarehouseRequestService;

        public ImportMainWarehouseRequestController(IImportMainWarehouseRequestService importMainWarehouseRequestService)
        {
            _importMainWarehouseRequestService = importMainWarehouseRequestService;
        }

        /// <summary>
        /// Get the list of ImportMainWarehouseRequest by deliveryStage id
        /// </summary>
        [HttpGet("deliveryStage/{deliveryStageId}")]
        public async Task<IActionResult> GetAllByPOId(int deliveryStageId)
        {
            var result = await _importMainWarehouseRequestService.GetAllByDeliveryStageIdAsync(deliveryStageId);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ImportMainWarehouseRequestAddVM requestAddVM)
        {
            await _importMainWarehouseRequestService.CreateAsync(requestAddVM);
            return Ok("Import main warehouse request create successfully");
        }

        [HttpPut("approval")]
        public async Task<IActionResult> UpdateApproveStatus(ImportMainWarehouseApproveRequestVM requestUpdateVM)
        {
            await _importMainWarehouseRequestService.UpdateApproveStatus(requestUpdateVM);
            return Ok("Import main warehouse request approve status updated successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(ImportMainWarehouseRequestUpdateVM requestUpdateVM)
        {
            await _importMainWarehouseRequestService.UpdateAsync(requestUpdateVM);
            return Ok("Import main warehouse request updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _importMainWarehouseRequestService.DeleteAsync(id);
            return Ok("Import main warehouse request deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _importMainWarehouseRequestService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _importMainWarehouseRequestService.GetAllAsync();
            return Ok(result);
        }
    }
}
