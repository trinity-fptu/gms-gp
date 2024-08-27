using Application.IRepos;
using Domain.Entities.UserRole;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class SupplierAccountRequestRepo : GenericRepo<SupplierAccountRequest>, ISupplierAccountRequestRepo
    {
        private readonly AppDbContext _context;

        public SupplierAccountRequestRepo(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<SupplierAccountRequest>> GetSupplierAccountRequestByPurchasingStaffId(int purchasingStaffId)
        {
            return await _context.SupplierAccountRequests
                .Where(x => 
                    x.RequestStaffId == purchasingStaffId 
                    && x.IsDeleted == false)
                .ToListAsync();
        }
    }
}
