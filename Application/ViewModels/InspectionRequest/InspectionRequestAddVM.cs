using Domain.Entities.UserRole;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.InspectionRequest
{
    public class InspectionRequestAddVM
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? RequestInspectDate { get; set; } 
        [JsonIgnore]
        public string? POCode { get; set; }
        public int? DeliveryStageId { get; set; }

    }
}
