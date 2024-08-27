using Domain.CommonBase;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Warehouse : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public WarehouseTypeEnum WarehouseType { get; set; }
        public ICollection<WarehouseMaterial>? WarehouseMaterials { get; set; }
    }
}
