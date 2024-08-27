using Application.IServices.IInspectionServices;
using Application.ViewModels.InspectionRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Inspection
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionRequestController : ControllerBase
    {
        private readonly IInspectionRequestService _inspectionRequestService;

        public InspectionRequestController(IInspectionRequestService inspectionRequestService)
        {
            _inspectionRequestService = inspectionRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _inspectionRequestService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _inspectionRequestService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] InspectionRequestAddVM inspectionRequestVM)
        {
            await _inspectionRequestService.CreateAsync(inspectionRequestVM);
            return Ok("Create inspection request successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] InspectionRequestUpdateVM requestDTO)
        {
            await _inspectionRequestService.UpdateAsync(requestDTO);
            return Ok("Update inspection request successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _inspectionRequestService.DeleteAsync(id);
            return Ok("Delete inspection request successfully");
        }

        [HttpPut("approve")]
        public async Task<IActionResult> UpdateApproveStatus([FromBody] InspectionApproveRequestVM approveRequestVM)
        {
            await _inspectionRequestService.UpdateApproveStatus(approveRequestVM);
            return Ok("Approve inspection request successfully");
        }
    }
}
