using Application.ViewModels.WarehouseFormMaterial;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.RawMaterialEnum;

namespace Application.ViewModels.InspectionForm.MaterialInspectResult
{
    public class MaterialInspectResultVM
    {
        public int Id { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialCode { get; set; }
        public double RequestQuantity { get; set; }
        public double MaterialPerPackage { get; set; }
        public double? InspectionPassQuantity { get; set; }
        public double? InspectionFailQuantity { get; set; }
        public string? Note { get; set; }
        public MaterialInspectResultEnum? InspectStatus { get; set; } = MaterialInspectResultEnum.Pending;


        public int? PurchaseMaterialId { get; set; }
        public int? InspectionFormId { get; set; }
        public PurchaseMaterialInFormVM? PurchaseMaterial { get; set; }
    }

}
