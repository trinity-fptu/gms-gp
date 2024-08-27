using Application.ViewModels.User;
using Domain.Entities;
using Domain.Entities.UserRole;

namespace Application.IServices
{
    public interface IUserService
    {
        Task CreateAsync(UserAddVM model);
        Task CreateAsync(UserSupplierAddVM model);
        Task<List<UserVM>> GetAllAsync();
        Task<List<UserVM>> GetAllByRoleAsync(int roleId);
        Task<UserVM> GetByIdAsync(int id);
        Task<UserVM> GetBySupplierId(int supplierId);
        Task<UserVM> GetByManagerId(int managerId);
        Task<UserVM> GetByPurchasingManagerId(int purchasingManagerId);
        Task<UserVM> GetByPurchasingStaffId(int purchasingStaffId);
        Task<UserVM> GetByWarehouseStaffId(int warehouseStaffId);
        Task<UserVM> GetByInspectorId(int inspectorId);
        Task<UserVM> GetByStaffCodeAsync(string code);
        Task UpdateAsync(UserUpdateVM model);
        Task UpdateAsync(UserSupplierUpdateVM model);
        Task DeleteAsync(int id);
        Task BanUserAsync(int id);
        Task UnbanUserAsync(int id);

        Task<string> LoginAsync(UserLoginVM model);
    }
}
