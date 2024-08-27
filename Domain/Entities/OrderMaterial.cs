using Domain.CommonBase;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Domain.Entities
{
    public class OrderMaterial : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? MaterialName { get; set; }
        public double PackageQuantity { get; set; }
        public double PackagePrice { get; set; }
        public double MaterialPerPackage { get; set; }
        public double? TotalPrice { get; set; }

        public int PurchasingOrderId { get; set; }
        public virtual PurchasingOrder? PurchasingOrder { get; set; }
        public int RawMaterialId { get; set; }
        public virtual RawMaterial? RawMaterial { get; set; }
    }
}
