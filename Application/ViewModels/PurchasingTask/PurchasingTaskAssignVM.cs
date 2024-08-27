using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Domain.Enums.PurchasingTaskEnum;

namespace Application.ViewModels.PurchasingTask
{
    public class PurchasingTaskAssignVM
    {
        public int Id { get; set; }
        [JsonIgnore]
        public DateTime AssignDate { get; set; } = DateTime.Now;
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }
        [JsonIgnore]
        public PurchasingTaskStatus TaskStatus { get; set; } = PurchasingTaskStatus.Assigned;
        [JsonIgnore]
        public double FinishedQuantity { get; set; } = 0;
        [JsonIgnore]
        public double RemainedQuantity { get; set; }
        public int PurchasingStaffId { get; set; }
    }
}
