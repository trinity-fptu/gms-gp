using Domain.CommonBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExpectedMaterial : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public double RequireQuantity { get; set; }

        public int ProductionPlanId { get; set; }
        public ProductionPlan? ProductionPlan { get; set; }
        public int RawMaterialId { get; set; }
        public RawMaterial? RawMaterial { get; set; } 
    }
}
