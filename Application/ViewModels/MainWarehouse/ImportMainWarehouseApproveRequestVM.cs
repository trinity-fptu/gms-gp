using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MainWarehouse
{
    public class ImportMainWarehouseApproveRequestVM
    {
        public int Id { get; set; }
        public ApproveEnum? ApproveStatus { get; set; }
        public string? RejectReason { get; set; }
        public DateTime? StatusUpdateDate { get; set; } = DateTime.Today;
       
    }
}
