using Notifications.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    class DefaultNotificationBuilder : INotificationBuilder
    {
        public NotificationPayload Build(Notification notification)
        {
            NotificationPayload payload = new NotificationPayload()
            {
                Title = notification.Title,
                Message = notification.Message,
                ObjectId = notification.Product.Id
            };

            if (!string.IsNullOrEmpty(notification.Sound))
            {
                payload.Sound = notification.Sound;
            }

            if (!string.IsNullOrEmpty(notification.Action))
            {
                payload.Action = notification.Action;
            }
            return payload;
        }
    }
}
