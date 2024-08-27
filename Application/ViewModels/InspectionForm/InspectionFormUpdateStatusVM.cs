using Application.ViewModels.InspectionForm.MaterialInspectResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.InspectionForm
{
    public class InspectionFormUpdateStatusVM
    {
        public int Id { get; set; }
        public string? InspectLocation { get; set; }
        public string? ResultNote { get; set; }

        public List<MaterialInspectResultUpdateStatusVM> MaterialInspectResults { get; set; }
    }
}
