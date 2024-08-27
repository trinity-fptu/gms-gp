using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.ProductCategory;

namespace Application.IServices
{
    public interface IProductCategoryService
    {
        Task CreateAsync(ProductCategoryAddVM productCategoryAddVM);
        Task<ProductCategoryVM> GetByIdAsync(int id);
        Task<List<ProductCategoryVM>> GetAllAsync();
        Task UpdateAsync(ProductCategoryUpdateVM productCategoryUpdateVM);
        Task DeleteAsync(int id);
    }
}
