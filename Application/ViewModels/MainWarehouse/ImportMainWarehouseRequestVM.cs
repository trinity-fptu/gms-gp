using Domain.Enums.Warehousing;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.WarehouseForm;
using Domain.Entities.UserRole;
using Application.ViewModels.DeliveryStage;

namespace Application.ViewModels.MainWarehouse
{
    public class ImportMainWarehouseRequestVM
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string? POCode { get; set; }
        public ApproveEnum ApproveStatus { get; set; }
        public string? RejectReason { get; set; }
        public string? RequestTitle { get; set; }
        public string? RequestReasonContent { get; set; }
        public DateTime RequestExecutionDate { get; set; }
        public WarehouseFormVM WarehouseForm { get; set; }
        public int? InspectorId { get; set; }
        public int? ApproveWStaffId { get; set; }
        public virtual DeliveryStageInRequestVM? DeliveryStage { get; set; }
    }
}
