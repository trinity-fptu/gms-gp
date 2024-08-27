using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public class PurchasingTaskEnum
    {
        public enum PurchasingTaskStatus
        {
            Pending = 1,
            Processing = 2,
            Finished = 3,
            Assigned = 4,
            Overdue = 5,
        }
    }
}
