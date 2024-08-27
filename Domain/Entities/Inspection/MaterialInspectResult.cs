using Domain.CommonBase;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Inspection
{
    public class MaterialInspectResult : BaseTimeInfoEntity
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

        public int PurchaseMaterialId { get; set; }
        public PurchaseMaterial? PurchaseMaterial { get; set; }
        public int InspectionFormId { get; set; }
        public InspectionForm? InspectionForm { get; set; }
    }
}
