using Domain.CommonBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MaterialCategory : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RawMaterial>? RawMaterials { get; set; }     
    }
}
