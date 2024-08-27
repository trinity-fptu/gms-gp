

using Application.ViewModels.WarehouseForm;
using Domain.Entities.Warehousing;
using Domain.Enums.Warehousing;
using Microsoft.AspNetCore.Http;

namespace Application.IServices.WarehousingServices
{
    public interface IWarehouseFormService
    {
        Task<WarehouseFormVM> GetByTempWarehouseRequestIdAsync(int tempWarehouseRequestId);
        Task<WarehouseFormVM> GetByImportMainWarehouseRequestIdAsync(int importMainWarehouseRequestId);
        Task<List<WarehouseFormVM>> GetByPOCodeAsync(string poCode);
        Task CreateAsync(WarehouseFormAddVM warehouseFormAddVM);
        Task<WarehouseFormVM> GetByIdAsync(int id);
        Task<List<WarehouseFormVM>> GetAllAsync();
        Task UpdateAsync(WarehouseFormUpdateVM warehouseFormUpdateVM);
        Task DeleteAsync(int id);
        //Task<WarehouseFormAddVM> CreateFromFileAsync(IFormFile formFile);
        Task CreateTempExportWarehouseFormByDeliveryStage(int deliveryStageId);
        Task CreateTempWarehouseform(int requestId);
        Task CreateImportMainWarehouseformByDeliveryStageId(int deliveryStageId);
        Task CreateImportMainWarehouseform(int requestId);
        Task<byte[]> GenerateImportWarehouseFormExcelFile(int warehouseFormId, byte[] templateFileBytes);
        Task<byte[]> GenerateExportWarehouseFormExcelFile(int warehouseFormId, byte[] templateFileBytes);
        Task UpdateTempExportFormStatusAsync(int formId);
        Task UpdateTempImportFormStatusAsync(int formId);
        Task UpdateMainImportFormStatusAsync(int formId);
    }
}
