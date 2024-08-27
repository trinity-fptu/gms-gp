using Application.IServices;
using Application.ViewModels.Warehouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var warehouse = await _warehouseService.GetByIdAsync(id);
            return Ok(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouses()
        {
            var warehouses = await _warehouseService.GetAllAsync();
            return Ok(warehouses);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WarehouseAddVM model)
        {
            await _warehouseService.CreateAsync(model);
            return Ok("Warehouse created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(WarehouseUpdateVM model)
        {
            await _warehouseService.UpdateAsync(model);
            return Ok("Warehouse updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _warehouseService.DeleteAsync(id);
            return Ok("Warehouse deleted successfully");
        }

        [HttpGet("main/{warehouseId}/{rawMaterialId}")]
        public async Task<IActionResult> GetMaterialInMainWarehouse(int warehouseId, int rawMaterialId)
        {
            var materialInWarehouse = await _warehouseService.GetPurchaseMaterialListInMainWarehouse(warehouseId, rawMaterialId);
            return Ok(materialInWarehouse);
        }

        [HttpGet("temp/{warehouseId}/{rawMaterialId}")]
        public async Task<IActionResult> GetMaterialInTempnWarehouse(int warehouseId, int rawMaterialId)
        {
            var materialInWarehouse = await _warehouseService.GetPurchaseMaterialListInTempWarehouse(warehouseId, rawMaterialId);
            return Ok(materialInWarehouse);
        }
    }
}
