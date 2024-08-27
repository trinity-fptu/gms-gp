using Application.IServices.InspectionServices;
using Application.ViewModels.InspectionForm;
using Application.ViewModels.WarehouseForm;
using Domain.Enums.Warehousing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Inspection
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionFormController : ControllerBase
    {
        private readonly IInspectionFormService _inspectionFormService;
        private readonly IWebHostEnvironment _env;

        public InspectionFormController(IInspectionFormService inspectionFormService, IWebHostEnvironment env)
        {
            _inspectionFormService = inspectionFormService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(InspectionFormAddVM inspectionFormVM)
        {
            await _inspectionFormService.CreateAsync(inspectionFormVM);
            return Ok("Inspection form create successfully");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(InspectionFormUpdateVM inspectionFormVM)
        {
            await _inspectionFormService.UpdateAsync(inspectionFormVM);
            return Ok("Inspection form update successfully");
        }

        [HttpPost("from-request/{inspectionRequestId}")]
        public async Task<IActionResult> CreateInspectionForm(int inspectionRequestId)
        {
            await _inspectionFormService.CreateInspectionForm(inspectionRequestId);
            return Ok("Warehouse form material created successfully");
        }

        [HttpPut("inspection-form-status")]
        public async Task<IActionResult> UpdateTempImportFormStatusAsync(InspectionFormUpdateStatusVM inspectionFormDTO)
        {
            await _inspectionFormService.UpdateInspectionFormStatusAsync(inspectionFormDTO);
            return Ok("Warehouse form updated status successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _inspectionFormService.DeleteAsync(id);
            return Ok("Inspection form delete successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _inspectionFormService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _inspectionFormService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Export the inspection form to the excel file by inspection form id
        /// </summary>
        [HttpGet("{id}/file/export-inspection-form")]
        public async Task<IActionResult> GetFileFromInspectionFormId(int id)
        {
            // Combine the wwwroot path with the file name
            string templateFilePath = Path.Combine(_env.WebRootPath, "InspectionFormTemplate.xlsx");
            // Read the file content
            byte[] templateFileBytes = System.IO.File.ReadAllBytes(templateFilePath);
            var resultFile = await _inspectionFormService.GenerateInspectionFormExcelFile(id, templateFileBytes);
            var fileName = $"InspectionForm_{id}.xlsx";
            return File(resultFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet("POCode/{poCode}")]
        public async Task<IActionResult> GetByPOCode(string poCode)
        {
            var result = await _inspectionFormService.GetByPOCodeAsync(poCode);
            return Ok(result);
        }
    }
}
