using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Application.ViewModels.WarehouseMaterial
{
    public class MainWarehouseMaterialVM
    {
        public int? RawMaterialId { get; set; }
        public string? RawMaterialName { get; set; }
        public string? POCode { get; set; }
        public int? StageOrder { get; set; }
        public string? PMCode { get; set; }
        public RawMaterialUnitEnum? Unit { get; set; }
        public double? Quantity { get; set; }
    }
}
