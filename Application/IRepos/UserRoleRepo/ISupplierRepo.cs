
using Domain.Entities.UserRole;

namespace Application.IRepos.UserRoleRepo
{
    public interface ISupplierRepo : IGenericRepo<Supplier>
    {
        Task<Supplier> GetSupplierByIdAsync(int id);
    }
}
