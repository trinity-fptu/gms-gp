using Application.IServices;
using Application.Services;
using Application.ViewModels.ProductionPlan;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanService _productionPlanService;

        public ProductionPlanController(IProductionPlanService productionPlanService)
        {
            _productionPlanService = productionPlanService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductionPlanVM>>> GetAll()
        {
            var result = await _productionPlanService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get the list of ProductionPlanVM that has no purchasing plan yet
        /// </summary>
        [HttpGet("no-purchasing-plan-yet")]
        public async Task<ActionResult<List<ProductionPlanVM>>> GetAllWithoutPlan()
        {
            var result = await _productionPlanService.GetAllPlanWithoutPurchasingPlanAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionPlanVM>> GetById(int id)
        {
            var result = await _productionPlanService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productionPlanService.DeleteAsync(id);
            return Ok("Production plan deleted successfully");
        }

        /// <summary>
        /// Upload production plan file and import data to database
        /// </summary>
        [HttpPost("file")]
        public async Task<IActionResult> Import(IFormFile formFile)
        {
            var result = await _productionPlanService.ImportProductionPlanFile(formFile);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductionPlanAddVM productionPlanAddVM)
        {
            await _productionPlanService.CreateAsync(productionPlanAddVM);
            return Ok("Production plan created successfully");
        }
    }
}
