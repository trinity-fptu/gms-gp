using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Domain.Enums.DeliveryStage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.DeliveryStage
{
    public class DeliveryStageUpdateVM
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        [Range(0, double.MaxValue)]
        public int StageOrder { get; set; }
        [JsonIgnore]
        public int TotalTypeMaterial { get; set; } = 0;
        [JsonIgnore]
        public double TotalPrice { get; set; } = 0;
        public DeliveryStageStatusEnum? Status { get; set; }
        public List<PurchaseMaterialUpdateVM> PurchaseMaterials { get; set; }
    }
}
