using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WarehouseMaterial
{
    public class WarehouseMaterialVM
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public double? ReturnQuantity { get; set; } = 0;
        public double TotalPrice { get; set; }
        public int? RawMaterialId { get; set; }
        public int? WarehouseId { get; set; }
        public RawMaterialVMforWarehouseMaterial RawMaterial { get; set; }
    }

    public class RawMaterialVMforWarehouseMaterial
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double? EstimatePrice { get; set; }
        public string? StorageGuide { get; set; }
        public string? ImageUrl { get; set; }
    }
}
