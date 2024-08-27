using Domain.CommonBase;
using Domain.Entities.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.PurchasingTaskEnum;

namespace Domain.Entities
{
    public class PurchasingTask : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }
        public PurchasingTaskStatus? TaskStatus { get; set; } = PurchasingTaskStatus.Pending;
        public double? FinishedQuantity { get; set; }
        public double? RemainedQuantity { get; set; }
        public double? ProcessedQuantity { get; set; }

        public int PurchasingPlanId { get; set; }
        public virtual PurchasingPlan? PurchasingPlan { get; set; }
        public int RawMaterialId { get; set; }
        public virtual RawMaterial? RawMaterial { get; set; }
        public int? PurchasingStaffId { get; set; }
        public virtual PurchasingStaff? PurchasingStaff { get; set; }
    }
}
