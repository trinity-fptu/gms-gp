using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Application.ViewModels.DeliveryStage.PurchaseMaterial
{
    public class PurchaseMaterialAddVM
    {
        public string? MaterialName { get; set; }
        [Range(0, double.MaxValue)]
        public double PackagePrice { get; set; } = 0;
        [Range(0, double.MaxValue)]
        public double TotalQuantity { get; set; }
        public int? RawMaterialId { get; set; }
        [JsonIgnore]
        public double TotalPrice { get; set; }

    }
}
