using Domain.Enums.Warehousing;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TempWarehouse
{
    public class TempWarehouseApproveRequestVM
    {
        public int Id { get; set; }
        public ApproveEnum ApproveStatus { get; set; }
        public string? RejectReason { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Today;
        public DateTime ApproveExecutionDate { get; set; }
    }
}
