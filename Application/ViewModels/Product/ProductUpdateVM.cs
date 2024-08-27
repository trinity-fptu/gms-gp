using Application.ViewModels.Product.ProductMaterial;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Application.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public ProductUnitEnum Unit { get; set; }
        public string? Description { get; set; }

        public ProductSizeEnum? ProductSize { get; set; }

        public int? ProductCategoryId { get; set; }

        public List<ProductMaterialUpdateVM>? ProductMaterials { get; set; }
    }
}
