using Application.IRepos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class ProductMaterialRepo : GenericRepo<ProductMaterial>, IProductMaterialRepo
    {
        public ProductMaterialRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ProductMaterial>> GetByProductIdAsync(int id)
        {
            return await _dbSet
                .Where(x =>
                    x.ProductId == id
                    && !x.IsDeleted).ToListAsync();
        }
    }
}
