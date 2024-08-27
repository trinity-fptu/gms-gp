using Application.IServices;
using Application.Services;
using Application.ViewModels.RawMaterial;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RawMaterialController : ControllerBase
    {
        private readonly IRawMaterialService _rawMaterialService;

        public RawMaterialController(IRawMaterialService rawMaterialService)
        {
            _rawMaterialService = rawMaterialService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRawMaterialById(int id)
        {
            var rawMaterial = await _rawMaterialService.GetByIdAsync(id);
            return Ok(rawMaterial);
        }

        /// <summary>
        /// Get the list of RawMaterial by materialCategory id
        /// </summary>
        [HttpGet("materialCategory/{materialCategoryId}")]
        public async Task<IActionResult> GetRawMaterialByMaterialCategoryId(int materialCategoryId)
        {
            var rawMaterials = await _rawMaterialService.GetRawMaterialByMaterialCategoryId(materialCategoryId);
            return Ok(rawMaterials);
        }

        [HttpGet]
        public async Task<IActionResult> GetRawMaterials()
        {
            var rawMaterials = await _rawMaterialService.GetAllAsync();
            return Ok(rawMaterials);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RawMaterialAddVM model)
        {
            await _rawMaterialService.CreateAsync(model);
            return Ok("Raw material created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(RawMaterialVM model)
        {
            await _rawMaterialService.UpdateAsync(model);
            return Ok("RawMaterial updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rawMaterialService.DeleteAsync(id);
            return Ok("Raw material deleted successfully");
        }

    }
}
