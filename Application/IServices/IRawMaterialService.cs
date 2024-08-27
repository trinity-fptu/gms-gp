
using Application.ViewModels.RawMaterial;

namespace Application.IServices
{
    public interface IRawMaterialService
    {
        Task CreateAsync(RawMaterialAddVM rawMaterialAddVM);
        Task<RawMaterialVM> GetByIdAsync(int id);
        Task<List<RawMaterialVM>> GetRawMaterialByMaterialCategoryId(int materialCategoryId);
        Task<List<RawMaterialVM>> GetAllAsync();
        Task UpdateAsync(RawMaterialVM rawMaterialVM);
        Task DeleteAsync(int id);
    }
}
