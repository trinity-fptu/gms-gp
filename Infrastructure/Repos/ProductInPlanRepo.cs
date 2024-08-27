using Application.IRepos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class ProductInPlanRepo : GenericRepo<ProductInPlan>, IProductInPlanRepo
    {
        public ProductInPlanRepo(AppDbContext context) : base(context)

        {
        }
    }
}
