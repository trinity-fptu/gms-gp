using Domain.CommonBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Domain.Entities
{
    public class Product : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ProductUnitEnum Unit { get; set; }
        public string? Note { get; set; }
        public string? Description { get; set; }

        public ProductSizeEnum? ProductSize { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public ICollection<ProductInPlan>? ProductInPlans { get; set; }
        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}
