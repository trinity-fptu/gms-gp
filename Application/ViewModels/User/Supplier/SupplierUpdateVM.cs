using Application.ViewModels.BaseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.User.Supplier
{
    public class SupplierUpdateVM : UpdateTimeVM
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyTaxCode { get; set; }
    }
}
