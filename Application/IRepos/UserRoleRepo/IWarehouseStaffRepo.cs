
using Domain.Entities.UserRole;

namespace Application.IRepos.UserRoleRepo
{
    public interface IWarehouseStaffRepo : IGenericRepo<WarehouseStaff>
    {
        Task<WarehouseStaff> GetWarehouseStaffByIdAsync(int id);
    }
}
