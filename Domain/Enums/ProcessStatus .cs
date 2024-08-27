using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ProcessStatus
    {
        Pending = 0,
        Processing = 1,
        Finished = 2,
        Rejected = 3,
        Overdue = 4,
    }
}