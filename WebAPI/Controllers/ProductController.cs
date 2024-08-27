using Application.IServices;
using Application.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            var result = await _productService.GetByCodeAsync(code);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductAddVM product)
        {
            await _productService.AddAsync(product);
            return Ok("Add new product success");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductUpdateVM product)
        {
            await _productService.UpdateAsync(product);
            return Ok("Update product success");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok("Delete product success");
        }   
    }
}
