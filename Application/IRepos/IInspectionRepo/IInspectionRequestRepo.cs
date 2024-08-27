using Domain.Entities.Inspect;


namespace Application.IRepos.IInspectionRepo
{
    public interface IInspectionRequestRepo : IGenericRepo<InspectionRequest>
    {
        Task<List<InspectionRequest>> GetAllByDeliveryStageIdAsync(int deliveryStageId);
        Task<List<InspectionRequest>> GetAllWithDetailAsync();
        Task<InspectionRequest> GetByIdWithInspectionFormAsync(int id);
    }
}
