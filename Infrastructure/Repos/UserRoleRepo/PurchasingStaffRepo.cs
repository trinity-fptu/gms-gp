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
    public class PurchasingStaffRepo : GenericRepo<PurchasingStaff>, IPurchasingStaffRepo
    {
        public PurchasingStaffRepo(AppDbContext context) : base(context)
        { 
        }

        public async Task<PurchasingStaff> GetPurchasingStaffByIdAsync(int id)
        {
            var item = await _dbSet.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            return item;
        }
    }
}
