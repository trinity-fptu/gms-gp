using Application.ViewModels.MaterialCategory;
using Application.ViewModels.RawMaterial;

namespace Application.IServices
{
    public interface IMaterialCategoryService
    {

        Task CreateAsync(MaterialCategoryAddVM materialCategoryAddVM);
        Task<MaterialCategoryVM> GetByIdAsync(int id);
        Task<List<MaterialCategoryVM>> GetAllAsync();
        Task UpdateAsync(MaterialCategoryVM materialCategoryVM);
        Task DeleteAsync(int id);
    }
}
