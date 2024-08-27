using Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IProductService
    {
        public Task<List<ProductVM>> GetAllAsync();
        public Task<ProductVM> GetByIdAsync(int id);
        public Task<ProductVM> GetByCodeAsync(string code);
        public Task AddAsync(ProductAddVM product);
        public Task UpdateAsync(ProductUpdateVM product);
        public Task DeleteAsync(int id);
    }
}
