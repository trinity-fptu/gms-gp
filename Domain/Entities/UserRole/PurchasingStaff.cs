using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CommonBase;
using Domain.Entities.Inspect;
using Domain.Entities.Warehousing;

namespace Domain.Entities.UserRole
{
    public class PurchasingStaff : BaseTimeInfoEntity
    {
        public int Id { get; set; }

        public User? User { get; set; }
        public ICollection<PurchasingOrder>? PurchasingOrders { get; set; }
        public ICollection<TempWarehouseRequest>? TempWarehouseRequests { get; set; }
        public ICollection<PurchasingTask>? PurchasingTasks { get; set; }
        public ICollection<SupplierAccountRequest>? SupplierAccountRequests { get; set; }
        public ICollection<InspectionRequest>? InspectionRequests { get; set; }
        public ICollection<PO_Report>? PO_Reports { get; set; }
    }
}
