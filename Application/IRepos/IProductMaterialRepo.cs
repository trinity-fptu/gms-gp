using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepos
{
    public interface IProductMaterialRepo : IGenericRepo<ProductMaterial>
    {
        Task<List<ProductMaterial>> GetByProductIdAsync(int id);
    }
}
