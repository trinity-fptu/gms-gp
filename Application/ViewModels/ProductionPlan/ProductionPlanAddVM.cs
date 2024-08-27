using Application.ViewModels.ExpectedMaterial;
using Application.ViewModels.ProductInPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductionPlan
{
    public class ProductionPlanAddVM
    {
        public string Name { get; set; } = "";
        public DateTime PlanStartDate { get; set; } = DateTime.Now;
        public DateTime? PlanEndDate { get; set; }
        public string? Note { get; set; }
        public string? FileUrl { get; set; }
        public List<ProductInPlanAddVM>? ProductInPlans { get; set; }
        public List<ExpectedMaterialAddVM>? ExpectedMaterials { get; set;}
    }
}
