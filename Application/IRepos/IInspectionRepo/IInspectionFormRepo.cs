using Domain.Entities.Inspection;


namespace Application.IRepos.IInspectionRepo
{
    public interface IInspectionFormRepo : IGenericRepo<InspectionForm>
    {
        public Task<InspectionForm> GetByIdWithDetailAsync(int id);
        public Task<List<InspectionForm>> GetAllWithDetailAsync();

    }
}
