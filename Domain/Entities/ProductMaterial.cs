using Domain.CommonBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductMaterial : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public double? Area { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int RawMaterialId { get; set; }
        public RawMaterial? RawMaterial { get; set; }
    }
}
