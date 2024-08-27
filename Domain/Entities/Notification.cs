using Domain.CommonBase;
using Domain.Enums;
using Domain.Enums.Warehousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notification : BaseTimeInfoEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public NotificationActionEnum Action { get; set; }
        public NotificationTypeEnum Type { get; set; }
        public NotificationStatusEnum Status { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int? PurchasingOrderId { get; set; }
        public virtual PurchasingOrder? PurchasingOrder { get; set; }
    }
}
