using Domain.Entities.UserRole;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.PO_ReportEnums;

namespace Application.ViewModels.PO_Report
{
    public class PO_ReportAddVM
    {
        public string? ReportTitle { get; set; }
        public string? ReportContent { get; set; }
        public int SupplierId { get; set; }
        public int PurchasingOrderId { get; set; }
    }
}
