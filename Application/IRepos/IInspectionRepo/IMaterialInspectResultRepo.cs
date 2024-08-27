using Domain.Entities.Inspection;


namespace Application.IRepos.IInspectionRepo
{
    public interface IMaterialInspectResultRepo : IGenericRepo<MaterialInspectResult>
    {
        Task<List<MaterialInspectResult>> GetAllByInspectionFormIdAsync(int inspectionFormId);
        Task<MaterialInspectResult> GetByPmId(int id);
    }
}
