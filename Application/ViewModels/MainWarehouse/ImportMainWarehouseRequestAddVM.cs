using Domain.Enums.Warehousing;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MainWarehouse
{
    public class ImportMainWarehouseRequestAddVM
    {
        public DateTime RequestDate { get; set; }
        public string? RequestTitle { get; set; }
        public string? RequestReasonContent { get; set; }
        public DateTime RequestExecutionDate { get; set; }
        public int DeliveryStageId { get; set; }
    }
}
