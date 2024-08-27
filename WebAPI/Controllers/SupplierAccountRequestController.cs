using Application.IServices;
using Application.Services;
using Application.ViewModels.SupplierAccountRequest;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierAccountRequestController : ControllerBase
    {
        private readonly ISupplierAccountRequestService _supplierAccountRequestService;

        public SupplierAccountRequestController(ISupplierAccountRequestService supplierAccountRequestService)
        {
            _supplierAccountRequestService = supplierAccountRequestService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierAccountRequestById(int id)
        {
            var accountRequest = await _supplierAccountRequestService.GetByIdAsync(id);
            return Ok(accountRequest);
        }

        [HttpGet("staff/{id}")]
        public async Task<IActionResult> GetSupplierAccountRequestByPurchasingStaffId(int id)
        {
            var accountRequest = await _supplierAccountRequestService.GetSupplierAccountRequestByPurchasingStaffId(id);
            return Ok(accountRequest);
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplierAccountRequests()
        {
            var accountRequest = await _supplierAccountRequestService.GetAllAsync();
            return Ok(accountRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierAccountRequestAddVM model)
        {
            await _supplierAccountRequestService.CreateAsync(model);
            return Ok("Account request created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(SupplierAccountRequestUpdateVM model)
        {
            await _supplierAccountRequestService.UpdateAsync(model);
            return Ok("Account request updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierAccountRequestService.DeleteAsync(id);
            return Ok("Account request deleted successfully");
        }

        [HttpGet("approve/{id}/{approvingEnums}")]
        public async Task<IActionResult> ChangeApprovalStatus(int id, ApproveEnum approvingEnums)
        {
            await _supplierAccountRequestService.ChangeApprovalStatus(id, approvingEnums);
            return Ok("Account request approved status change successfully");
        }
    }
}