using Application.ViewModels.BaseVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PurchasingTask
{
    public class PurchasingTaskUpdateVM : UpdateTimeVM
    {
        public int Id { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int? RawMaterialId { get; set; }
        public double Quantity { get; set; }

        public int? PurchasingStaffId { get; set; }
    }
}
