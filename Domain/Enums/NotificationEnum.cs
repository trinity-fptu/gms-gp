using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum NotificationActionEnum
    {
        Test, // This is just a test enum, no purpose
    }

    public enum NotificationTypeEnum
    {
        ProductionPlan,
        PurchasingPlan,
        PurchasingOrder,
        DeliveryStage,
        Warehousing,
        Inspection,
    }

    public enum NotificationStatusEnum
    {
        Unread,
        Read,
    }
}
