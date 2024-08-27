using Domain.CommonBase;
using Domain.Entities.Inspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Inspection
{
    public class InspectionForm : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? ResultCode { get; set; }
        public string? POCode { get; set; }
        public string? InspectLocation { get; set; }
        public string? InspectorName { get; set; }
        public string? ResultNote { get; set; }
        public string? ManagerName { get; set; }

        public int InspectionRequestId { get; set; }
        public InspectionRequest? InspectionRequest { get; set; }
        public ICollection<MaterialInspectResult>? MaterialInspectResults { get; set; }

    }
}
