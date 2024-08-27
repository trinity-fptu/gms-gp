using Domain.CommonBase;
using Domain.Entities.UserRole;
using Domain.Enums;
using Domain.Enums.PurchasingOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PurchasingOrder : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? POCode { get; set; }
        public ApproveEnum? SupplierApproveStatus { get; set; } = ApproveEnum.Pending;
        public ApproveEnum? ManagerApproveStatus { get; set; } = ApproveEnum.Pending;
        public string? SupplierName { get; set; }
        public string? SupplierCompanyName { get; set; }
        public string? SupplierTaxCode { get; set; }
        public string? SupplierAddress { get; set; }
        public string? SuppplierEmail { get; set; }
        public string? SupplierPhone { get; set; }
        public string? ReceiverCompanyPhone { get; set; }
        public string? ReceiverCompanyEmail { get; set; }
        public string? ReceiverCompanyAddress { get; set; }
        public string? Note { get; set; }
        public int? NumOfDeliveryStage { get; set; } = 0;
        public int? TotalMaterialType { get; set; } = 0;
        public double? TotalPrice { get; set; } = 0;
        public PurchasingOrderStatusEnum? OrderStatus { get; set; }

        public int PurchasingPlanId { get; set; }
        public virtual PurchasingPlan? PurchasingPlan { get; set; }
        public int PurchasingStaffId { get; set; }
        public virtual PurchasingStaff? PurchasingStaff { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public ICollection<PO_Report>? PO_Reports { get; set; }
        public ICollection<DeliveryStage>? DeliveryStages { get; set; }
        public ICollection<OrderMaterial>? OrderMaterials { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
