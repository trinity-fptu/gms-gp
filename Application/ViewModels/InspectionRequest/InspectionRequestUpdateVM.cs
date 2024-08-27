using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.InspectionRequest
{
    public class InspectionRequestUpdateVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? POCode { get; set; }
        public DateTime? RequestInspectDate { get; set; } = DateTime.Now;


    }
}
