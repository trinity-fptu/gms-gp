using Domain.Enums.Warehousing;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MainWarehouse
{
    public class ImportMainWarehouseRequestUpdateVM
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public ApproveEnum ApproveStatus { get; set; }
        public string? RejectReason { get; set; }
        public string? RequestTitle { get; set; }
        public DateTime RequestExecutionDate { get; set; }
        public ImportMainWarehouseRequestEnum Status { get; set; }
    }
}
