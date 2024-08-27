using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Application.ViewModels.DeliveryStage.PurchaseMaterialInRequest;
using Domain.Enums.DeliveryStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DeliveryStage
{
    public class DeliveryStageInRequestVM
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int StageOrder { get; set; }
        public int TotalTypeMaterial { get; set; } = 0;
        public double TotalPrice { get; set; } = 0;
        public DeliveryStageStatusEnum? DeliveryStatus { get; set; }
        public bool IsSupplemental { get; set; } = false;
        public List<PurchaseMaterialInRequestVM> PurchaseMaterials { get; set; }
    }
}
