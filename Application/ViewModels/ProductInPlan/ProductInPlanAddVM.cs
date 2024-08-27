using Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductInPlan
{
    public class ProductInPlanAddVM
    {
        public double Quantity { get; set; }

        public int? ProductId { get; set; }
    }
}
