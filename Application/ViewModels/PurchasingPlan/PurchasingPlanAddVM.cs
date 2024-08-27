using Application.ValidateAttributes;
using Application.ViewModels.PurchasingTask;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PurchasingPlan
{
    public class PurchasingPlanAddVM
    {
        public string Title { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Note { get; set; }
        public int? ProductionPlanId { get; set; }

        public List<PurchasingTaskAddVM> PurchaseTasks { get; set; }
    }
}
