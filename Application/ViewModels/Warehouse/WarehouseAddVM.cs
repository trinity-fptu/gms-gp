using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Warehouse
{
    public class WarehouseAddVM
    {
        public string Location { get; set; }
        public WarehouseTypeEnum WarehouseType { get; set; }
    }
}
