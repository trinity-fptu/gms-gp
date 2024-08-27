using Domain.Enums.Warehousing;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TempWarehouse
{
    public class TempWarehouseRequestUpdateVM
    {
        public int Id { get; set; }
        public DateTime? UpdateDate { get; set; } = DateTime.Now;
        public string? RequestTitle { get; set; }
        public string? RequestReasonContent { get; set; }
        public WarehouseRequestTypeEnum? RequestType { get; set; }
        public DateTime? RequestExecutionDate { get; set; }
        public int DeliveryStageId { get; set; }
    }
}
