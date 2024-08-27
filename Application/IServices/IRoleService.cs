
using Application.ViewModels.Role;

namespace Application.IServices
{
    public interface IRoleService
    {
        Task CreateAsync(RoleAddVM roleAddVM);
        Task<List<RoleVM>> GetAllAsync();
        Task<RoleVM> GetByIdAsync(int id);
        Task UpdateAsync(RoleVM roleVM);
    }
}
