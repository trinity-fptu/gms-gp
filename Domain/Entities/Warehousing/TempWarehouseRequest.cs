using Domain.CommonBase;
using Domain.Entities.UserRole;
using Domain.Enums;
using Domain.Enums.Warehousing;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Warehousing
{
    public class TempWarehouseRequest : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? POCode { get; set; }
        public ApproveEnum? ApproveStatus { get; set; } = ApproveEnum.Pending;
        public string? RejectReason { get; set; }
        public string? RequestTitle { get; set; }
        public string? RequestReasonContent { get; set; }
        public WarehouseRequestTypeEnum? RequestType { get; set; }
        public DateTime? RequestExecutionDate { get; set; }

        public int DeliveryStageId { get; set; }
        public virtual DeliveryStage? DeliveryStage { get; set; }
        public int? RequestStaffId { get; set; }
        public virtual PurchasingStaff? RequestStaff { get; set; }
        public int? RequestInspectorId { get; set; }
        public virtual Inspector? RequestInspector { get; set; }
        public int? ApproveWStaffId { get; set; }
        public virtual WarehouseStaff? ApproveWStaff { get; set; }
        public WarehouseForm? WarehouseForm { get; set; }
    }
}
