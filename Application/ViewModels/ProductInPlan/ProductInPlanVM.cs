using Application.ViewModels.Product;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductInPlan
{
    public class ProductInPlanVM
    {
        public int Id { get; set; }
        public double Quantity { get; set; }

        public int? ProductionPlanId { get; set; }
        public int? ProductId { get; set; }

        public ProductVM? Product { get; set; }
    }
}
