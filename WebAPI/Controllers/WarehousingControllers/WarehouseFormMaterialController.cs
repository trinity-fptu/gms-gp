using Application.Exceptions;
using Application.IServices.WarehousingServices;
using Application.ViewModels.WarehouseFormMaterial;
using Domain.Enums;
using Domain.Enums.Warehousing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.WarehousingControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseFormMaterialController : ControllerBase
    {
        private readonly IWarehouseFormMaterialService _warehouseFormMaterialService;

        public WarehouseFormMaterialController(IWarehouseFormMaterialService warehouseFormMaterialService)
        {
            _warehouseFormMaterialService = warehouseFormMaterialService;
        }

        [HttpGet("{id}/updateStatus/{status}")]
        public async Task<IActionResult> UpdateStatusAsync(int id, WarehouseFormStatusEnum status)
        {
            await _warehouseFormMaterialService.UpdateStatus(id, status);
            return Ok("Warehouse form material status update successfully");
        }

    }
}
