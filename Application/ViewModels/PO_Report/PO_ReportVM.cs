using Domain.Entities.UserRole;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.PO_ReportEnums;
using Application.ViewModels.BaseVM;

namespace Application.ViewModels.PO_Report
{
    public class PO_ReportVM : BaseEntityVM
    {
        public int Id { get; set; }
        public ResolveEnums ResolveStatus { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportContent { get; set; }
        public string? ReportAnswer { get; set; }
        public int? SupplierId { get; set; }
        public int? PurchasingOrderId { get; set; }
        public int? PurchasingStaffId { get; set; }
        public ApproveEnum? SupplierApprovingStatus { get; set; }
    }
}
