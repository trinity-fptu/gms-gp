using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderMaterialController : ControllerBase
    {
        private readonly IOrderMaterialService _orderMaterialService;

        public OrderMaterialController(IOrderMaterialService orderMaterialService)
        {
            _orderMaterialService = orderMaterialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderMaterialService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderMaterialService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of Order Material for a purchasingOrder
        /// </summary>
        [HttpGet("purchasingOrder/{purchasingOrderId}")]
        public async Task<IActionResult> GetOrderMaterialByPOId(int purchasingOrderId)
        {
            var result = await _orderMaterialService.GetOrderMaterialByPOId(purchasingOrderId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderMaterialService.DeleteAsync(id);
            return Ok("Order material delete success");
        }
    }
}
