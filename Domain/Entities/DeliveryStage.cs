using Domain.CommonBase;
using Domain.Entities.Inspect;
using Domain.Entities.Warehousing;
using Domain.Enums.DeliveryStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DeliveryStage : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? StageOrder { get; set; }
        public int? TotalTypeMaterial { get; set; }
        public double TotalPrice { get; set; }
        public DeliveryStageStatusEnum? DeliveryStatus { get; set; } = DeliveryStageStatusEnum.Pending;
        public bool IsSupplemental { get; set; } = false;

        public int? PurchasingOrderId { get; set; }
        public virtual PurchasingOrder? PurchasingOrder { get; set; }
        public ICollection<PurchaseMaterial>? PurchaseMaterials { get; set; }
        public ICollection<TempWarehouseRequest>? TempWarehouseRequests { get; set; }
        public ICollection<ImportMainWarehouseRequest>? ImportMainWarehouseRequests { get; set; }
        public ICollection<InspectionRequest>? InspectionRequests { get; set; }
        public ICollection<WarehouseForm>? WarehouseForms { get; set; }
    }
}
