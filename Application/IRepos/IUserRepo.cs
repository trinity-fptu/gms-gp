using Domain.Entities;

namespace Application.IRepos
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> Login(string email, string hashedPassword);

        Task<User> GetByIdAsync(int id);

        Task<User> GetBySupplierId(int supplierId);
        Task<User> GetByManagerId(int managerId);
        Task<User> GetByPurchasingManagerId(int purchasingManagerId);
        Task<User> GetByPurchasingStaffId(int purchasingStaffId);
        Task<User> GetByWarehouseStaffId(int warehouseStaffId);
        Task<User> GetByInspectorId(int inspectorId);
        Task<List<User>> GetAllByRoleId(int roleId);
        Task<User> GetByStaffCodeAsync(string code);
    }
}
