
using Domain.Entities.UserRole;

namespace Application.IRepos.UserRoleRepo
{

    public interface IInspectorRepo : IGenericRepo<Inspector>
    {
        Task<Inspector> GetInspectorByIdAsync(int id);
    }
}
