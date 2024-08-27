using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BaseVM
{
    public class BaseEntityVM
    {
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
