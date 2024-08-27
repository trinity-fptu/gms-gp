using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Application.ViewModels.InspectionRequest;
using Application.ViewModels.WarehouseForm;
using Domain.Enums.DeliveryStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DeliveryStage
{
    public class DeliveryStageWithInspectionRequestVM
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int StageOrder { get; set; }
        public int TotalTypeMaterial { get; set; } = 0;
        public double TotalPrice { get; set; } = 0;
        public DeliveryStageStatusEnum? DeliveryStatus { get; set; }
        public bool IsSupplemental { get; set; } = false;
        public int? PurchasingOrderId { get; set; }
        public List<PurchaseMaterialVM> PurchaseMaterials { get; set; }
        public InspectionRequestVM InspectionRequest { get; set; }

        public WarehouseFormVM ImportMainWarehouseForm { get; set; }
        public WarehouseFormVM ExportTempWarehouseForm { get; set; }
    }
}
