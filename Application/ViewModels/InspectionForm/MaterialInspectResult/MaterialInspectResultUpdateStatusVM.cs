using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.InspectionForm.MaterialInspectResult
{
    public class MaterialInspectResultUpdateStatusVM
    {
        public int Id { get; set; }
        public double? InspectionPassQuantity { get; set; }
        public double? InspectionFailQuantity { get; set; }
        public string? Note { get; set; }
    }
}
