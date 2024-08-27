using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.WarehouseMaterial
{
    public class UpdateWarehouseFormMaterial
    {
        public int Id { get; set; }
        [Range(0, double.MaxValue)]

        [JsonIgnore]
        public double TotalPrice { get; set; }
        public DateTime? ExecutionDate { get; set; } = DateTime.Now;
    }
}
