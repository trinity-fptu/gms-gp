using Domain.Enums.Warehousing;
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
    public class PurchaseMaterialUpdateVM
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = "";
        public string? CompanyMaterialCode { get; set; } = "";
        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; } = 0;
        public DateTime? TempImportDate { get; set; }
        public DateTime? TempExportDate { get; set; }
        public DateTime? MainImportDate { get; set; }
        public DateTime? MainExportDate { get; set; }
        public DateTime? PlanInspectDate { get; set; }

        public WarehouseStatusEnum? WarehouseStatus { get; set; }
        [Range(0, double.MaxValue)]
        public double? TotalQuantity { get; set; }
        public int? RawMaterialId { get; set; }
        [JsonIgnore]
        [Range(0, double.MaxValue)]
        public double? TotalPrice { get; set; }
    }
}
