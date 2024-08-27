using Application.ViewModels.BaseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.PurchasingTaskEnum;

namespace Application.ViewModels.PurchasingTask
{
    public class PurchasingTaskVM : BaseEntityVM
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public DateTime? AssignDate { get; set; } = DateTime.Now;
        public DateTime? FinishedDate { get; set; } = DateTime.Now;
        public PurchasingTaskStatus? TaskStatus { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }
        public double? FinishedQuantity { get; set; }
        public double? RemainedQuantity { get; set; }
        public double? ProcessedQuantity { get; set; }

        public int? PurchasingPlanId { get; set; }
        public int? RawMaterialId { get; set; }
        public int? PurchasingStaffId { get; set; }
    }
}
