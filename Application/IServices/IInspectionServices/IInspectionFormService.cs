

using Application.ViewModels.InspectionForm;
using Application.ViewModels.WarehouseForm;
using Microsoft.AspNetCore.Http;

namespace Application.IServices.InspectionServices
{
    public interface IInspectionFormService
    {
        Task CreateAsync(InspectionFormAddVM inspectionFormVM);
        Task CreateInspectionForm(int requestId);
        Task UpdateAsync(InspectionFormUpdateVM inspectionFormVM);
        Task UpdateInspectionFormStatusAsync(InspectionFormUpdateStatusVM inspectionFormDTO);
        Task DeleteAsync(int id);
        Task<InspectionFormVM> GetByIdAsync(int id);
        Task<List<InspectionFormVM>> GetAllAsync();
        Task<byte[]> GenerateInspectionFormExcelFile(int inspectionFormId, byte[] templateFileBytes);
        Task<List<InspectionFormVM>> GetByPOCodeAsync(string poCode);
    }
}
