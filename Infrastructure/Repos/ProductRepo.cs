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
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<Product> GetByCodeAsync(string code)
        {
            var item = await _dbSet
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductMaterials.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(x => x.Code == code && !x.IsDeleted);

            return item;
        }


        public async Task<Product> GetByIdWithDetailAsync(int id)
        {
            var item = await _dbSet
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductMaterials.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            return item;
        }

        public async Task<List<Product>> GetAllWithDetailAsync()
        {
            var items = await _dbSet
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductMaterials.Where(x => !x.IsDeleted))
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return items;
        }
    }
}
