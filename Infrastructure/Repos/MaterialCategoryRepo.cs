using Application.IRepos;
using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class MaterialCategoryRepo : GenericRepo<MaterialCategory>, IMaterialCategoryRepo
    {
        public MaterialCategoryRepo(AppDbContext context) : base(context)
        {
        }
    }
}
