using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.PO_ReportEnums;

namespace Application.ViewModels.PO_Report
{
    public class PO_ReportPurchasingStaffUpdateVM
    {
        public int Id { get; set; }
        public string? ReportAnswer { get; set; }
        public int PurchasingStaffId { get; set; }
    }
}
