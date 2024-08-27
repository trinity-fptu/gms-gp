using Application.ViewModels.Product;
using Application.ViewModels.Product.ProductMaterial;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IProductMaterialService
    {
        public Task<List<ProductMaterialVM>> GetAllAsync();
        public Task<List<ProductMaterialVM>> GetByProductIdAsync(int id);
    }
}
