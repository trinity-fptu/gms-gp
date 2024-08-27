using Application.IRepos.UserRoleRepo;
using Domain.Entities.UserRole;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.UserRoleRepo
{
    public class InspectorRepo : GenericRepo<Inspector>, IInspectorRepo
    {
        public InspectorRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<Inspector> GetInspectorByIdAsync(int id)
        {
            var item = await _dbSet.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            return item;
        }
    }
}
