using Application.ViewModels.DeliveryStage;
using Application.ViewModels.PurchasingOrder.OrderMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.PurchasingOrder
{
    public class PurchasingOrderAddVM
    {
        public string? Name { get; set; }
        public int? SupplierId { get; set; }
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

        [JsonIgnore]
        public int? NumOfDeliveryStage { get; set; } = 0;
        [JsonIgnore]
        public int? TotalMaterialType { get; set; } = 0;
        [JsonIgnore]
        public double? TotalPrice { get; set; } = 0;

        public int? PurchasingPlanId { get; set; }
        public List<OrderMaterialAddVM>? OrderMaterials { get; set; }
        public List<DeliveryStageAddVM>? DeliveryStages { get; set; }
    }
}
