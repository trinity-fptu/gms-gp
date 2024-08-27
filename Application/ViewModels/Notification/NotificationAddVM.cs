using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Notification
{
    public class NotificationAddVM
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public NotificationActionEnum Action { get; set; }
        public NotificationTypeEnum Type { get; set; }
        public NotificationStatusEnum Status { get; set; } = NotificationStatusEnum.Unread;

        public int UserId { get; set; }
        public int? PurchasingOrderId { get; set; }
    }
}
