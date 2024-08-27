using Application.ViewModels.BaseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Product.ProductMaterial
{
    public class ProductMaterialUpdateVM : UpdateTimeVM
    {
        public int Id { get; set; }
        public double Quantity { get; set; }

        public int ProductId { get; set; }
        public int RawMaterialId { get; set; }
    }
}
