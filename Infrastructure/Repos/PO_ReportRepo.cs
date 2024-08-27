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
    public class PO_ReportRepo : GenericRepo<PO_Report>, IPO_ReportRepo
    {
        private readonly AppDbContext _context;
        public PO_ReportRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PO_Report>> GetPOReportByPOId(int purchasingOrderId)
        {
            return await _context.PO_Reports
                .Where(x => x.PurchasingOrderId == purchasingOrderId && x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<PO_Report>> GetPOReportBySupplierId(int supplierId)
        {
            return await _context.PO_Reports
                .Where(x => 
                    x.SupplierId == supplierId 
                    && x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<PO_Report>> GetPOReportByStaffId(int staffId)
        {
            return await _context.PO_Reports
                .Where(x =>
                    x.PurchasingStaffId == staffId
                    && x.IsDeleted == false)
                .ToListAsync();
        }
    }
}
