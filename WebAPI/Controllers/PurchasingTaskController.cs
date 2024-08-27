using Application.IServices;
using Application.ViewModels.PurchasingTask;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasingTaskController : ControllerBase
    {
        private readonly IPurchasingTaskService _purchasingTaskService;

        public PurchasingTaskController(IPurchasingTaskService purchasingTaskService)
        {
            _purchasingTaskService = purchasingTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _purchasingTaskService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _purchasingTaskService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of purchasing task by purchasing plan id
        /// </summary>
        [HttpGet("purchasingPlan/{purchasingPlanId}")]
        public async Task<IActionResult> GetPurchasingTaskByPurchasingPlanId(int purchasingPlanId)
        {
            var result = await _purchasingTaskService.GetPurchasingTaskByPurchasingPlanId(purchasingPlanId);
            return Ok(result);
        }
        
        /// <summary>
        /// Get the list of purchasing task by purchasing staff id
        /// </summary>
        [HttpGet("purchasingStaff/{purchasingStaffId}")]
        public async Task<IActionResult> GetPurchasingTaskByPurchasingStaffId(int purchasingStaffId)
        {
            var result = await _purchasingTaskService.GetPurchasingTaskByPurchasingStaffId(purchasingStaffId);
            return Ok(result);
        }

        /// <summary>
        /// Assign a purchasing task to a purchasing staff 
        /// </summary>
        [HttpPut("assign")]
        public async Task<IActionResult> AssignTask(PurchasingTaskAssignVM model)
        {
            await _purchasingTaskService.AssignAsync(model);
            return Ok("Task assigned successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _purchasingTaskService.DeleteAsync(id);
            return Ok("Purchasing task deleted successfully");
        }
    }
}
