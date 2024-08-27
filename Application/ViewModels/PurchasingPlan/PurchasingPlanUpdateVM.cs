using Application.ViewModels.BaseVM;
using Application.ViewModels.PurchasingTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PurchasingPlan
{
    public class PurchasingPlanUpdateVM : UpdateTimeVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Note { get; set; }

        public List<PurchasingTaskUpdateVM>? PurchaseTasks { get; set; }
    }
}
