using Application.IRepos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class ProductCategoryRepo : GenericRepo<ProductCategory>, IProductCategoryRepo
    {
        public ProductCategoryRepo(AppDbContext context) : base(context)
        {
        }
    }
}
