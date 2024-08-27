using Application.IServices.WarehousingServices;
using Application.Services.WarehousingServices;
using Application.ViewModels.WarehouseForm;
using Domain.Enums.Warehousing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.WarehousingControllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseFormController : ControllerBase
    {
        private readonly IWarehouseFormService _warehouseFormService;
        private readonly IWebHostEnvironment _env;

        public WarehouseFormController(IWarehouseFormService warehouseFormService, IWebHostEnvironment env)
        {
            _warehouseFormService = warehouseFormService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _warehouseFormService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _warehouseFormService.GetByIdAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// Get the infor of warehouse form by tempWarehouseRequest id
        /// </summary>
        [HttpGet("tempWarehouseRequest/{tempWarehouseRequestId}")]
        public async Task<IActionResult> GetAllByTempWarehouseRequest(int tempWarehouseRequestId)
        {
            var result = await _warehouseFormService.GetByTempWarehouseRequestIdAsync(tempWarehouseRequestId);
            return Ok(result);
        }

        /// <summary>
        /// Get the infor of warehouse form by importMainWarehouseRequest id
        /// </summary>
        [HttpGet("importMainWarehouseRequest/{importMainWarehouseRequestId}")]
        public async Task<IActionResult> GetAllByImportMainWarehouseRequest(int importMainWarehouseRequestId)
        {
            var result = await _warehouseFormService.GetByImportMainWarehouseRequestIdAsync(importMainWarehouseRequestId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(WarehouseFormUpdateVM warehouseFormUpdateVM)
        {
            await _warehouseFormService.UpdateAsync(warehouseFormUpdateVM);
            return Ok("Warehouse form updated successfully");
        }

        /// <summary>
        /// Export the import warehouse form to the excel file by warehouse form id
        /// </summary>
        [HttpGet("{id}/file/export-import-warehouse-form")]
        public async Task<IActionResult> GetFileFromImportWarehouseFormId(int id)
        {
            // Combine the wwwroot path with the file name
            string templateFilePath = Path.Combine(_env.WebRootPath, "ImportWarehouseFormTemplate.xlsx");
            // Read the file content
            byte[] templateFileBytes = System.IO.File.ReadAllBytes(templateFilePath);
            var resultFile = await _warehouseFormService.GenerateImportWarehouseFormExcelFile(id, templateFileBytes);
            var fileName = $"WarehouseForm_{id}.xlsx";
            return File(resultFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        /// <summary>
        /// Export the export warehouse form to the excel file by warehouse form id
        /// </summary>
        [HttpGet("{id}/file/export-export-warehouse-form")]
        public async Task<IActionResult> GetFileFromExportWarehouseFormId(int id)
        {
            // Combine the wwwroot path with the file name
            string templateFilePath = Path.Combine(_env.WebRootPath, "ExportWarehouseFormTemplate.xlsx");
            // Read the file content
            byte[] templateFileBytes = System.IO.File.ReadAllBytes(templateFilePath);

            var resultFile = await _warehouseFormService.GenerateExportWarehouseFormExcelFile(id, templateFileBytes);
            var fileName = $"WarehouseForm_{id}.xlsx";
            return File(resultFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _warehouseFormService.DeleteAsync(id);
            return Ok("Warehouse form deleted successfully");
        }

        /// <summary>
        /// Create a import/export temporary warehouse form 
        /// </summary>
        [HttpPost("temp-warehouse-form/{tempWarehouseRequestId}")]
        public async Task<IActionResult> CreateTempWarehouseform(int tempWarehouseRequestId)
        {
            await _warehouseFormService.CreateTempWarehouseform(tempWarehouseRequestId);
            return Ok("Warehouse form material created successfully");
        }

        /// <summary>
        /// Create a import/export temporary warehouse form 
        /// </summary>
        [HttpPost("temp-export-warehouse-form/delivery-stage/{deliveryStageId}")]
        public async Task<IActionResult> CreateTempExportWarehouseformByDeliveryStageId(int deliveryStageId)
        {
            await _warehouseFormService.CreateTempExportWarehouseFormByDeliveryStage(deliveryStageId);
            return Ok("Warehouse form created successfully");
        }
        /// <summary>
        /// Create a import/export temporary warehouse form 
        /// </summary>
        [HttpPost("main-import-warehouse-form/delivery-stage/{deliveryStageId}")]
        public async Task<IActionResult> CreateMainWarehouseformByDeliveryStageId(int deliveryStageId)
        {
            await _warehouseFormService.CreateImportMainWarehouseformByDeliveryStageId(deliveryStageId);
            return Ok("Warehouse form created successfully");
        }

        /// <summary>
        /// Create a import main warehouse form 
        /// </summary>
        [HttpPost("import-main-warehouse-form/{importMainWarehouseRequestId}")]
        public async Task<IActionResult> CreateImportMainWarehouseform(int importMainWarehouseRequestId)
        {
            await _warehouseFormService.CreateImportMainWarehouseform(importMainWarehouseRequestId);
            return Ok("Warehouse form material created successfully");
        }

        /// <summary>
        /// Update a import temp warehouse form status
        /// </summary>
        [HttpPut("temp-import-form-status/{formId}")]
        public async Task<IActionResult> UpdateTempImportFormStatusAsync(int formId)
        {
            await _warehouseFormService.UpdateTempImportFormStatusAsync(formId);
            return Ok("Warehouse form updated status successfully");
        }

        /// <summary>
        /// Update a import main warehouse form status
        /// </summary>
        [HttpPut("main-import-form-status/{formId}")]
        public async Task<IActionResult> Update(int formId)
        {
            await _warehouseFormService.UpdateMainImportFormStatusAsync(formId);
            return Ok("Warehouse form updated status successfully");
        }

        /// <summary>
        /// Update a export temp warehouse form status
        /// </summary>
        [HttpPut("temp-export-form-status/{formId}")]
        public async Task<IActionResult> UpdateTempExportFormStatusAsync(int formId)
        {
            await _warehouseFormService.UpdateTempExportFormStatusAsync(formId);
            return Ok("Warehouse form updated status successfully");
        }

        [HttpGet("POCode/{poCode}")]
        public async Task<IActionResult> GetByPOCode(string poCode)
        {
            var result = await _warehouseFormService.GetByPOCodeAsync(poCode);
            return Ok(result);
        }
    }
}
