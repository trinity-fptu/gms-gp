using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.Warehouse
{
    public class WarehouseUpdateVM
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Location { get; set; }
        public WarehouseTypeEnum WarehouseType { get; set; }
    }
}
