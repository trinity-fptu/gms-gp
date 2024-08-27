using Application.IServices;
using Application.ViewModels.PO_Report;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PO_ReportController : ControllerBase
    {
        private readonly IPO_ReportService _pO_ReportService;

        public PO_ReportController(IPO_ReportService pO_ReportService)
        {
            _pO_ReportService = pO_ReportService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPO_ReportById(int id)
        {
            var pO_Report = await _pO_ReportService.GetByIdAsync(id);
            return Ok(pO_Report);
        }

        /// <summary>
        /// Get the list of purchase order report by purchasingOrder id
        /// </summary>
        [HttpGet("purchasingOrder/{purchasingOrderId}")]
        public async Task<IActionResult> GetPOReportByPOId(int purchasingOrderId)
        {
            var pO_Report = await _pO_ReportService.GetPOReportByPOId(purchasingOrderId);
            return Ok(pO_Report);
        }

        /// <summary>
        /// Get the list of purchase order report by supplier id
        /// </summary>
        [HttpGet("supplier/{supplierId}")]
        public async Task<IActionResult> GetPOReportBySupplierId(int supplierId)
        {
            var pO_Report = await _pO_ReportService.GetPOReportBySupplierId(supplierId);
            return Ok(pO_Report);
        }

        /// <summary>
        /// Get the list of purchase order report by create staff id
        /// </summary>
        [HttpGet("staff/{staffId}")]
        public async Task<IActionResult> GetPOReportByStaffId(int staffId)
        {
            var pO_Report = await _pO_ReportService.GetPOReportByStaffId(staffId);
            return Ok(pO_Report);
        }

        [HttpGet]
        public async Task<IActionResult> GetPO_Reports()
        {
            var pO_Report = await _pO_ReportService.GetAllAsync();
            return Ok(pO_Report);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PO_ReportAddVM model)
        {
            await _pO_ReportService.CreateAsync(model);
            return Ok("PO_Report created successfully");
        }

        [HttpPut("Supplier")]
        public async Task<IActionResult> UpdateForSupplier(PO_ReportSupplierUpdateVM model)
        {
            await _pO_ReportService.UpdateForSupplierAsync(model);
            return Ok("PO_Report updated successfully");
        }

        [HttpPut("PurchasingStaff")]
        public async Task<IActionResult> UpdateForStaff(PO_ReportPurchasingStaffUpdateVM model)
        {
            await _pO_ReportService.UpdateForPurchasingStaffAsync(model);
            return Ok("PO_Report updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pO_ReportService.DeleteAsync(id);
            return Ok("PO_Report deleted successfully");
        }

        [HttpPut("{id}/resolve")]
        public async Task<IActionResult> ChangeResolveStatus(int id, ApproveEnum resolveStatus)
        {
            await _pO_ReportService.ChangeResolveStatus(id, resolveStatus);
            return Ok("PO_Report resolve status change successfully");
        }
    }
}
