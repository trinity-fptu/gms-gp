using Domain.CommonBase;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Warehousing
{
    public class WarehouseFormMaterial : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialCode { get; set; }
        public double? RequestQuantity { get; set; }
        public double MaterialPerPackage { get; set; }
        public double? PackagePrice { get; set; }
        public double? TotalPrice { get; set; }
        public WarehouseFormStatusEnum? FormStatus { get; set; }
        public DateTime? ExecutionDate { get; set; }

        public int WarehouseFormId { get; set; }
        public WarehouseForm? WarehouseForm { get; set; }
        public int PurchaseMaterialId { get; set; }
        public PurchaseMaterial? PurchaseMaterial { get; set; }
    }
}
