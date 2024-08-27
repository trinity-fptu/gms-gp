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
    public class OrderMaterialDetailRepo : GenericRepo<OrderMaterial>, IOrderMaterialRepo
    {
        private readonly AppDbContext _context;

        public OrderMaterialDetailRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderMaterial>> GetOrderMaterialByPOId(int purchasingOrderId)
        {
            return await _context.OrderMaterials
                .Where(x => 
                    x.PurchasingOrderId == purchasingOrderId 
                    && x.IsDeleted == false)
                .ToListAsync();
        }
    }
}
