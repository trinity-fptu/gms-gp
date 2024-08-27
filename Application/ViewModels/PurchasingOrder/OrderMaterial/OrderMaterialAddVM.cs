using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.PurchasingOrder.OrderMaterial
{
    public class OrderMaterialAddVM
    {

        public string? MaterialName { get; set; }
        [Range(0, double.MaxValue)]
        public double PackageQuantity { get; set; }
        [Range(0, double.MaxValue)]
        public double PackagePrice { get; set; }
        public double MaterialPerPackage { get; set; }
        public int? RawMaterialId { get; set; }
        [JsonIgnore]
        public double? TotalPrice { get; set; }
    }
}
