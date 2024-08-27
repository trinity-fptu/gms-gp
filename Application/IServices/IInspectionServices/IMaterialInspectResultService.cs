

using Application.ViewModels.InspectionForm.MaterialInspectResult;
using Domain.Entities.Inspection;

namespace Application.IServices.IInspectionServices
{
    public interface IMaterialInspectResultService
    {
        Task<List<MaterialInspectResultVM>> GetAllByInspectionFormIdAsync(int inspectionFormId);
    }
}
