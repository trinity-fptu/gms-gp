using Application.ViewModels.DeliveryStage.PurchaseMaterial;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Application.ViewModels.WarehouseFormMaterial
{
    public class WarehouseFormMaterialVM
    {
        public int Id { get; set; }

        public int? PurchaseMaterialId { get; set; }
        public string MaterialName { get; set; } = "";
        public string MaterialCode { get; set; } = "";
        public double RequestQuantity { get; set; }
        public double MaterialPerPackage { get; set; }
        public double? PackagePrice { get; set; }
        public double TotalPrice { get; set; }
        public WarehouseFormStatusEnum? FormStatus { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public PurchaseMaterialInFormVM? PurchaseMaterial { get; set; }
    }

    public class PurchaseMaterialInFormVM
    {
        public string? Code { get; set; }
        public RawMaterialUnitEnum Unit { get; set; }
        public PackageUnitEnum Package { get; set; }
    }
}
