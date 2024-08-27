using Application.ViewModels.BaseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductionPlan
{
    public class ProductionPlanUpdateVM : UpdateTimeVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime PlanStartDate { get; set; } = DateTime.Now;
        public DateTime? PlanEndDate { get; set; }
        public string? Note { get; set; }
        public string? FileUrl { get; set; }
        public int? ManagerId { get; set; }
    }
}
