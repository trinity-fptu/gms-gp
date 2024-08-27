using Application.IServices;
using Application.Services;
using Application.ViewModels.PurchasingOrder;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasingOrderController : ControllerBase
    {
        private readonly IPurchasingOrderService _purchasingOrderService;

        public PurchasingOrderController(IPurchasingOrderService purchasingOrderService)
        {
            _purchasingOrderService = purchasingOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchasingOrderAddVM purchaseOrderAddVM)
        {
            await _purchasingOrderService.CreateAsync(purchaseOrderAddVM);
            return Ok("Purchasing order create successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var purchaseOrders = await _purchasingOrderService.GetAllAsync();
            return Ok(purchaseOrders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var purchaseOrder = await _purchasingOrderService.GetByIdAsync(id);
            return Ok(purchaseOrder);
        }

        /// <summary>
        /// Get the list of purchasing order by purchasing plan id
        /// </summary>
        [HttpGet("purchasingPlan/{purchasingPlanId}")]
        public async Task<IActionResult> GetPOByPurhcasingPlanId(int purchasingPlanId)
        {
            var result = await _purchasingOrderService.GetAllByPurchasingPlanIdAsync(purchasingPlanId);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of purchasing order by PO code
        /// </summary>
        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByPOCode(string code)
        {
            var result = await _purchasingOrderService.GetByPOCodeAsync(code);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _purchasingOrderService.DeleteAsync(id);
            return Ok("Purchasing order deleted successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(PurchasingOrderUpdateVM updateItem)
        {
            await _purchasingOrderService.UpdateAsync(updateItem);
            return Ok("Purchasing order updated successfully");
        }

        /// <summary>
        /// Approve the purchasing order with the given id to the given status
        /// </summary>
        [HttpGet("{id}/approval/{approvingStatus}")]
        public async Task<IActionResult> ApproveAsync(int id, ApproveEnum approvingStatus, bool isSupplier)
        {
            await _purchasingOrderService.ApproveAsync(approvingStatus, id, isSupplier);
            return Ok("Purchasing plan approved status change successfully");
        }

        /// <summary>
        /// Get the list of purchasing order by PO code
        /// </summary>
        [HttpGet("purchasingTask/{purchasingTaskId}")]
        public async Task<IActionResult> GetAllByPurchasingTaskIdAsync(int purchasingTaskId)
        {
            var result = await _purchasingOrderService.GetAllByPurchasingTaskIdAsync(purchasingTaskId);
            return Ok(result);
        }
    }
}
