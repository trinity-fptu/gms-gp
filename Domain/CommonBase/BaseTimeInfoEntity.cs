using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommonBase
{
    public abstract class BaseTimeInfoEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
        public string? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
