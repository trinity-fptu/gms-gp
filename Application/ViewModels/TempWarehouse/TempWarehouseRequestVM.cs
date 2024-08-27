using Domain.Enums.Warehousing;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.UserRole;
using Domain.Entities;
using Application.ViewModels.WarehouseForm;
using Application.ViewModels.DeliveryStage;

namespace Application.ViewModels.TempWarehouse
{
    public class TempWarehouseRequestVM
    {
        public int Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? POCode { get; set; }
        public ApproveEnum? ApproveStatus { get; set; }
        public string? RejectReason { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? RequestTitle { get; set; }
        public string? RequestReasonContent { get; set; }
        public WarehouseRequestTypeEnum? RequestType { get; set; }
        public DateTime? RequestExecutionDate { get; set; }
        public int? DeliveryStageId { get; set; }
        public int? RequestStaffId { get; set; }
        public int? RequestInspectorId { get; set; }
        public int? ApproveWStaffId { get; set; }
        public int? WarehouseFormId { get; set; }
        public WarehouseFormVM WarehouseForm { get; set; }

        public virtual DeliveryStageInRequestVM? DeliveryStage { get; set; }
    }
}
