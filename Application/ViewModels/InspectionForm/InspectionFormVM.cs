using Application.ViewModels.InspectionForm.MaterialInspectResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.InspectionForm
{
    public class InspectionFormVM
    {
        public int Id { get; set; }
        public string? ResultCode { get; set; }
        public string? POCode { get; set; }
        public string? InspectLocation { get; set; }
        public string? InspectorName { get; set; }
        public string? ResultNote { get; set; }
        public string? ManagerName { get; set; }

        public int? InspectionRequestId { get; set; }

        public List<MaterialInspectResultVM> MaterialInspectResults { get; set; }
    }
}
