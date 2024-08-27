using Application.IServices;
using Application.ViewModels.WarehouseMaterial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseMaterialController : ControllerBase
    {
        private readonly IWarehouseMaterialService _warehouseMaterialService;

        public WarehouseMaterialController(IWarehouseMaterialService warehouseMaterialService)
        {
            _warehouseMaterialService = warehouseMaterialService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseMaterialById(int id)
        {
            var warehouseMaterial = await _warehouseMaterialService.GetByIdAsync(id);
            return Ok(warehouseMaterial);
        }

        [HttpGet("warehouse/{warehouseId}")]
        public async Task<IActionResult> GetWarehouseMaterialsByWarehouseId(int warehouseId)
        {
            var warehouseMaterials = await _warehouseMaterialService.GetByWarehouseIdAsync(warehouseId);
            return Ok(warehouseMaterials);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseMaterials()
        {
            var warehouseMaterials = await _warehouseMaterialService.GetAllAsync();
            return Ok(warehouseMaterials);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WarehouseMaterialAddVM model)
        {
            await _warehouseMaterialService.CreateAsync(model);
            return Ok("Warehouse material created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(WarehouseMaterialUpdateVM model)
        {
            await _warehouseMaterialService.UpdateAsync(model);
            return Ok("Warehouse material updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _warehouseMaterialService.DeleteAsync(id);
            return Ok("Warehouse material deleted successfully");
        }

    }
}
