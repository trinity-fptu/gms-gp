using Domain.CommonBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserRole
{
    public class PurchasingManager : BaseTimeInfoEntity
    {
        public int Id { get; set; }

        public User? User { get; set; }
        public ICollection<PurchasingPlan>? PurchasingPlans { get; set; }
    }
}
