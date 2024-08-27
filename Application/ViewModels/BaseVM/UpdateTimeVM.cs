using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.BaseVM
{
    public class UpdateTimeVM
    {
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
    }
}
