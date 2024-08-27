
using Domain.Entities.UserRole;

namespace Application.IRepos.UserRoleRepo
{
    public interface IManagerRepo : IGenericRepo<Manager>
    {
        Task<Manager> GetManagerByIdAsync(int id);
    }
}
