using Application.ViewModels.BaseVM;
using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Application.ViewModels.PurchasingOrder;
using Domain.Enums.DeliveryStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DeliveryStage
{
    public class DeliveryStageVM : BaseEntityVM
    {
        public int Id { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int StageOrder { get; set; }
        public int TotalTypeMaterial { get; set; } = 0;
        public double TotalPrice { get; set; } = 0;
        public DeliveryStageStatusEnum? DeliveryStatus { get; set; }
        public bool IsSupplemental { get; set; } = false;
        public int? PurchasingOrderId { get; set; }
        public List<PurchaseMaterialVM> PurchaseMaterials { get; set; }
        public POTitleVM PurchasingOrder { get; set; }
    }

    public class POTitleVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? POCode { get; set; }
    }
}
