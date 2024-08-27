using Domain.CommonBase;
using Domain.Entities.Inspect;
using Domain.Entities.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserRole
{
    public class Inspector : BaseTimeInfoEntity
    {
        public int Id { get; set; }

        public User? User { get; set; }
        public ICollection<InspectionRequest>? InspectionRequests { get; set; }
        public ICollection<ImportMainWarehouseRequest>? ImportMainWarehouseRequests { get; set; }
        public ICollection<TempWarehouseRequest>? TempWarehouseRequests { get; set; }
    }
}
