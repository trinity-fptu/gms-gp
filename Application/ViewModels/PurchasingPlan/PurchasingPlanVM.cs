using Application.ViewModels.BaseVM;
using Application.ViewModels.ProductionPlan;
using Application.ViewModels.PurchasingOrder;
using Application.ViewModels.PurchasingTask;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PurchasingPlan
{
    public class PurchasingPlanVM : BaseEntityVM
    {
        public int Id { get; set; }
        public string PlanCode { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Note { get; set; }
        public ApproveEnum ApproveStatus { get; set; }
        public ProcessStatus ProcessStatus { get; set; }
        public int? ProductionPlanId { get; set; }
        public int? PurchasingManagerId { get; set; }
        public List<PurchasingTaskVM> PurchaseTasks { get; set; }
    }
}
