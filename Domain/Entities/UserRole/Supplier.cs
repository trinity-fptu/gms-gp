using Domain.CommonBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserRole
{
    public class Supplier : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyTaxCode { get; set; }

        public User? User { get; set; }
        public ICollection<PurchasingOrder>? PurchasingOrders { get; set; }
        public ICollection<PO_Report>? PO_Reports { get; set; }

    }
}
