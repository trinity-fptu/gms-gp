using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepos;
using Domain.Entities;

namespace Infrastructure.Repos
{
    public class RoleRepo : GenericRepo<Role>, IRoleRepo
    {
        public RoleRepo(AppDbContext context) : base(context)
        {
        }
    }
}
