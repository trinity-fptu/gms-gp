using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.WarehouseMaterial
{
    public class WarehouseMaterialAddVM
    {
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public double Quantity { get; set; }
        public int? RawMaterialId { get; set; }
        public int? WarehouseId { get; set; }
    }
}
