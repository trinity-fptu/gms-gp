using Application.ViewModels.DeliveryStage;
using Application.ViewModels.InspectionForm;
using Domain.Entities.Inspection;
using Domain.Entities.UserRole;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.InspectionRequest
{
    public class InspectionRequestVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? POCode { get; set; }
        public DateTime? RequestInspectDate { get; set; } = DateTime.Now;
        public ApproveEnum? ApproveStatus { get; set; } = ApproveEnum.Pending;
        public string? RejectReason { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public int? DeliveryStageId { get; set; }
        public int? RequestStaffId { get; set; }
        public int? ApprovingInspectorId { get; set; }

        public InspectionFormVM? InspectionForm { get; set; }
        public virtual DeliveryStageInRequestVM? DeliveryStage { get; set; }

    }
}
