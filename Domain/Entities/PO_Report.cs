using Domain.CommonBase;
using Domain.Entities.UserRole;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.PO_ReportEnums;

namespace Domain.Entities
{
    public class PO_Report : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public ResolveEnums? ResolveStatus { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportContent { get; set; }
        public string? ReportAnswer { get; set; }
        public ApproveEnum? SupplierApprovingStatus { get; set; }

        public int? SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public int? PurchasingStaffId { get; set; }
        public virtual PurchasingStaff? PurchasingStaff { get; set; }
        public int? PurchasingOrderId { get; set; }
        public virtual PurchasingOrder? PurchasingOrder { get; set; }

    }
}
