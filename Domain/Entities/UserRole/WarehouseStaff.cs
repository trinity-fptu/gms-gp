using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CommonBase;
using Domain.Entities.Warehousing;

namespace Domain.Entities.UserRole
{
    public class WarehouseStaff : BaseTimeInfoEntity
    {
        public int Id { get; set; }

        public User? User { get; set; }
        public ICollection<TempWarehouseRequest>? TempWarehouseRequests { get; set; }
        public ICollection<ImportMainWarehouseRequest>? ImportMainWarehouseRequests { get; set; }
    }
}
