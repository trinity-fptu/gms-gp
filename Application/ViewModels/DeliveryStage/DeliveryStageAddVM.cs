using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.DeliveryStage
{
    public class DeliveryStageAddVM
    {
        public DateTime? DeliveryDate { get; set; }
        public int? StageOrder { get; set; }
        [JsonIgnore]
        public int? TotalTypeMaterial { get; set; }
        [JsonIgnore]
        public double TotalPrice { get; set; }
        public List<PurchaseMaterialAddVM> PurchaseMaterials { get; set; }
    }
}
