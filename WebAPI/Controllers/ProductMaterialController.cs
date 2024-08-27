using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMaterialController : ControllerBase
    {
        private readonly IProductMaterialService _productionMaterialService;

        public ProductMaterialController(IProductMaterialService productionMaterialService)
        {
            _productionMaterialService = productionMaterialService;
        }

        /// <summary>
        /// Get the list of product material by productId id
        /// </summary>
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductMaterialByPRODUCTId(int productId)
        {
            var result = await _productionMaterialService.GetByProductIdAsync(productId);
            return Ok(result);
        }
    }
}
