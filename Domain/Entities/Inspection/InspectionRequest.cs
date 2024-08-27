using Domain.CommonBase;
using Domain.Entities.Inspection;
using Domain.Entities.UserRole;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Inspect
{
    public class InspectionRequest : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? POCode { get; set; }
        public DateTime? RequestInspectDate { get; set; } 
        public ApproveEnum? ApproveStatus { get; set; } = ApproveEnum.Pending;
        public string? RejectReason { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public int DeliveryStageId { get; set; }
        public virtual DeliveryStage? DeliveryStage { get; set; }
        public int RequestStaffId { get; set; }
        public virtual PurchasingStaff? RequestStaff { get; set; }
        public int? ApprovingInspectorId { get; set; }
        public virtual Inspector? ApprovingInspector { get; set; }
        public InspectionForm? InspectionForm { get; set; }
    }
}
