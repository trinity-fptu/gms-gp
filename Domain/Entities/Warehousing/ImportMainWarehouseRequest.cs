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
    public class ImportMainWarehouseRequest : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public DateTime? RequestDate { get; set; } = DateTime.Today;
        public string? POCode { get; set; }
        public ApproveEnum? ApproveStatus { get; set; } = ApproveEnum.Pending;
        public string? RejectReason { get; set; }
        public string? RequestTitle { get; set; }
        public string? RequestReasonContent { get; set; }
        public DateTime? RequestExecutionDate { get; set; }

        public int DeliveryStageId { get; set; }
        public virtual DeliveryStage? DeliveryStage { get; set; }
        public int InspectorId { get; set; }
        public virtual Inspector? Inspector { get; set; }
        public int? ApproveWStaffId { get; set; }
        public virtual WarehouseStaff? ApproveWStaff { get; set; }
        public WarehouseForm? WarehouseForm { get; set; }
    }
}
