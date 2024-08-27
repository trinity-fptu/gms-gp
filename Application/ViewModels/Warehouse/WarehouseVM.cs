using Application.ViewModels.WarehouseMaterial;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Warehouse
{
    public class WarehouseVM
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public WarehouseTypeEnum WarehouseType { get; set; }
        public List<WarehouseMaterialVM> WarehouseMaterials { get; set; }

    }
}
