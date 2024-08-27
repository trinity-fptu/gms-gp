using Domain.CommonBase;
using Domain.Entities.UserRole;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PurchasingPlan : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string PlanCode { get; set; }
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Note { get; set; }
        public ApproveEnum ApproveStatus { get; set; } = ApproveEnum.Pending;
        public ProcessStatus ProcessStatus { get; set; }
        public int? PurchasingManagerId { get; set; }

        public virtual PurchasingManager? PurchasingManager { get; set; }
        public int ProductionPlanId { get; set; }
        public virtual ProductionPlan? ProductionPlan { get; set; }
        public ICollection<PurchasingOrder>? PurchasingOrders { get; set; }
        public ICollection<PurchasingTask>? PurchaseTasks { get; set; }
    }
}
