using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.WarehouseFormMaterial
{
    public class WarehouseFormMaterialUpdateVM
    {
        public int Id { get; set; }
        public int? PurchaseMaterialId { get; set; }
        public string MaterialName { get; set; } = "";
        public string MaterialCode { get; set; } = "";
        public double RequestQuantity { get; set; }
        public double ReceiveQuantity { get; set; }
        [Range(0, double.MaxValue)]
        public double MaterialPerPackage { get; set; }
        [Range(0, double.MaxValue)]
        public double? PackagePrice { get; set; }
        [JsonIgnore]
        public double TotalPrice { get; set; }
    }
}
