using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ExpectedMaterial
{
    public class ExpectedMaterialVM
    {
        public int Id { get; set; }
        public double RequireQuantity { get; set; }

        public int? ProductionPlanId { get; set; }
        public int? RawMaterialId { get; set; }
    }
}
