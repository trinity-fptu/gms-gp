using Application.IServices;
using Application.ViewModels.PurchasingPlan;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasingPlanController : ControllerBase
    {
        private readonly IPurchasingPlanService _purchasingPlanService;
        public PurchasingPlanController(IPurchasingPlanService purchasingPlanService)
        {
            _purchasingPlanService = purchasingPlanService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PurchasingPlanAddVM model)
        {
            await _purchasingPlanService.CreateAsync(model);
            return Ok("Purchasing plan created successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _purchasingPlanService.GetByIdAsync(id);
            return Ok(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var result = await _purchasingPlanService.GetAllAsync();
        //    return Ok(result);
        //}

        /// <summary>
        /// Get the list of all purchasing plan
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAuthorzeAsync()
        {
            var result = await _purchasingPlanService.GetAllAuthorizeAsync();
            return Ok(result);
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetAllApprovedAsync()
        {
            var result = await _purchasingPlanService.GetAllApprovedAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PurchasingPlanUpdateVM model)
        {
            await _purchasingPlanService.UpdateAsync(model);
            return Ok("Purchasing plan updated successfully");
        }

        [HttpGet("{id}/approval/{approvingStatus}")]
        public async Task<IActionResult> ApproveAsync(int id, ApproveEnum approvingStatus)
        {
            await _purchasingPlanService.ApproveAsync(approvingStatus, id);
            return Ok("Purchasing plan approved status change successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _purchasingPlanService.DeleteAsync(id);
            return Ok("Purchasing plan deleted successfully");
        }
    }
}
