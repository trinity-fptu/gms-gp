using Application.ViewModels.BaseVM;
using Application.ViewModels.ExpectedMaterial;
using Application.ViewModels.ProductInPlan;
using Application.ViewModels.PurchasingPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductionPlan
{
    public class ProductionPlanVM : BaseEntityVM
    {
        public int Id { get; set; }
        public string ProductionPlanCode { get; set; }
        public string Name { get; set; } = "";
        public DateTime PlanStartDate { get; set; } = DateTime.Now;
        public DateTime? PlanEndDate { get; set; }
        public string? Note { get; set; }
        public string? FileUrl { get; set; }
        public int? ManagerId { get; set; }

        public List<ProductInPlanVM> ProductInPlans { get; set; }
        public List<ExpectedMaterialVM> ExpectedMaterials { get; set; }

        public PurchasingPlanVM? PurchasingPlan { get; set; }
    }
}
