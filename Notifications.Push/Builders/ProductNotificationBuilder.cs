using Notifications.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    class ProductNotificationBuilder : INotificationBuilder
    {
        private const string NAME_PLACEHOLDER = "{{product_name}}";

        public NotificationPayload Build(Notification notification)
        {
            string title = notification.Title.Replace(NAME_PLACEHOLDER, notification.Product.Name);
            NotificationPayload payload = new NotificationPayload()
            {
                Title = title,
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
