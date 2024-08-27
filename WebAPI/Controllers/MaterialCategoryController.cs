using Application.IServices;
using Application.ViewModels.MaterialCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialCategoryController : ControllerBase
    {
        private readonly IMaterialCategoryService _materialCategoryService;

        public MaterialCategoryController(IMaterialCategoryService materialCategoryService)
        {
            _materialCategoryService = materialCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _materialCategoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _materialCategoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MaterialCategoryAddVM materialCategoryAddVM)
        {
            await _materialCategoryService.CreateAsync(materialCategoryAddVM);
            return Ok("Material category created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(MaterialCategoryVM materialCategoryVM)
        {
            await _materialCategoryService.UpdateAsync(materialCategoryVM);
            return Ok("Material category updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _materialCategoryService.DeleteAsync(id);
            return Ok("Material category deleted successfully");
        }
    }
}
