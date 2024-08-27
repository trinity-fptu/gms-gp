using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PurchasingOrder.OrderMaterial
{
    public class OrderMaterialVM
    {
        public int Id { get; set; }
        public int? RawMaterialId { get; set; }
        public string MaterialName { get; set; } = "";
        public double PackageQuantity { get; set; }
        public double PackagePrice { get; set; }
        public double MaterialPerPackage { get; set; }
        public double TotalPrice { get; set; }
    }
}
