using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.InspectionForm.MaterialInspectResult
{
    public class MaterialInspectResultAddVM
    {
        [JsonIgnore]
        public string? MaterialName { get; set; }
        public string? MaterialCode { get; set; }
        public double RequestQuantity { get; set; }
        public double MaterialPerPackage { get; set; }
        public double? InspectionPassQuantity { get; set; }
        public double? InspectionFailQuantity { get; set; }
        public string? Note { get; set; }

        public int? PurchaseMaterialId { get; set; }
    }
}
