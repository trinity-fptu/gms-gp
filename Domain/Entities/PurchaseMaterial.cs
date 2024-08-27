using Domain.CommonBase;
using Domain.Entities.Inspection;
using Domain.Entities.UserRole;
using Domain.Entities.Warehousing;
using Domain.Enums.DeliveryStage;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Domain.Entities
{
    public class PurchaseMaterial : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? MaterialName { get; set; }
        public string? Code { get; set; }
        public string? CompanyMaterialCode { get; set; }
        public RawMaterialUnitEnum Unit { get; set; }
        public PackageUnitEnum Package { get; set; }
        public double MaterialPerPackage { get; set; }
        public double? TotalQuantity { get; set; }
        public double PackagePrice { get; set; }
        public double? DeliveredQuantity { get; set; }
        public double? ReturnQuantity { get; set; }
        public double? TotalPrice { get; set; }
        public double? AfterInspectQuantity { get; set; }
        public DateTime? TempImportDate { get; set; }
        public DateTime? TempExportDate { get; set; }
        public DateTime? MainImportDate { get; set; }
        public DateTime? PlanInspectDate { get; set; }
        public DeliveryStageStatusEnum? WarehouseStatus { get; set; }

        public int? DeliveryStageId { get; set; }
        public virtual DeliveryStage? DeliveryStage { get; set; }
        public int RawMaterialId { get; set; }
        public virtual RawMaterial? RawMaterial { get; set; }
        public int? WarehouseMaterialId { get; set; }
        public virtual WarehouseMaterial? WarehouseMaterial { get; set; }
        public ICollection<WarehouseFormMaterial>? WarehouseFormMaterials { get; set; }
        public MaterialInspectResult? MaterialInspectResult { get; set; }
    }
}
