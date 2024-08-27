using Domain.CommonBase;
using Domain.Entities.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductionPlan : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string ProductionPlanCode { get; set; }
        public DateTime PlanStartDate { get; set; } = DateTime.Now;
        public DateTime? PlanEndDate { get; set; }
        public string? Note { get; set; }
        public string? FileUrl { get; set; }
        public int? ManagerId { get; set; }
        public virtual Manager? Manager { get; set; }
        public virtual ICollection<PurchasingPlan> PurchasingPlans { get; set; }
        public virtual ICollection<ExpectedMaterial> ExpectedMaterials { get; set; }
        public virtual ICollection<ProductInPlan> ProductInPlans { get; set; }
    }
}
