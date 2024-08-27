using Domain.CommonBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WarehouseMaterial : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public double Quantity { get; set; } = 0;
        public double? ReturnQuantity { get; set; } = 0;
        public double? TotalPrice { get; set; }
        public int RawMaterialId { get; set; }
        public virtual RawMaterial? RawMaterial { get; set; }
        public int WarehouseId { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public ICollection<PurchaseMaterial>? PurchaseMaterials { get; set; }
    }
}
