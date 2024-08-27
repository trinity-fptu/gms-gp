using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.InspectionRequest
{
    public class InspectionApproveRequestVM
    {
        public int Id { get; set; }
        public ApproveEnum ApproveStatus { get; set; }
        public string? RejectReason { get; set; }
        public DateTime ApprovedDate { get; set; } = DateTime.Today;
    }
}
