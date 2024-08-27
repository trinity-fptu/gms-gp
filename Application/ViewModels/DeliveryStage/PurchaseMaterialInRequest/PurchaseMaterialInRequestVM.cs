using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Application.ViewModels.DeliveryStage.PurchaseMaterialInRequest
{
    public class PurchaseMaterialInRequestVM
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = "";
        public string? Code { get; set; }
        public int? RawMaterialId { get; set; }
        public string CompanyMaterialCode { get; set; } = "";
        public RawMaterialUnitEnum Unit { get; set; }
        public PackageUnitEnum Package { get; set; }
        public double MaterialPerPackage { get; set; }
        public double PackagePrice { get; set; } = 0;
        public double? TotalQuantity { get; set; }
        public double? DeliveredQuantity { get; set; }
        public double? AfterInspectQuantity { get; set; }

        public double? TotalPrice { get; set; }

    }
}
