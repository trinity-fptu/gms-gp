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
    public class PurchasingManagerRepo : GenericRepo<PurchasingManager>, IPurchasingManagerRepo
    {
        public PurchasingManagerRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<PurchasingManager> GetPurchasingManagerByIdAsync(int id)
        {
            var item = await _dbSet.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            return item;
        }
    }
}
