using Application.IServices;
using Application.ViewModels.ProductCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            var productCategory = await _productCategoryService.GetByIdAsync(id);
            return Ok(productCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductCategoriess()
        {
            var productCategories = await _productCategoryService.GetAllAsync();
            return Ok(productCategories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryAddVM model)
        {
            await _productCategoryService.CreateAsync(model);
            return Ok("Product category created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductCategoryUpdateVM model)
        {
            await _productCategoryService.UpdateAsync(model);
            return Ok("Product category updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productCategoryService.DeleteAsync(id);
            return Ok("Product category deleted successfully");
        }

    }
}
