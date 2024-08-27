using Application.IRepos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class ExpectedMaterialRepo : GenericRepo<ExpectedMaterial>, IExpectedMaterialRepo
    {
        public ExpectedMaterialRepo(AppDbContext context) : base(context)

        {
        }
    }
}
