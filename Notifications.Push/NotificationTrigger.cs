using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Data;

namespace Notifications.Push
{
    public class NotificationTrigger
    {
        private Sender sender;
        private IEnumerable<Notification> notifications;

        public NotificationTrigger(Sender sender, IEnumerable<Notification> notifications)
        {
            this.sender = sender;
            this.notifications = notifications;
        }

        public void TriggerIfNeeded()
        {
            IEnumerable<Notification> triggeredNotifications = this.notifications
                .Where(n =>
                {
                    INotificationCondition condition = NotificationConditionsFactory.Make(n);
                    return condition.isVerified();
                });
            foreach(Notification notification in triggeredNotifications)
            {
                INotificationBuilder builder = NotificationBuildersFactory.Make(notification.Builder);
                sender.Send(builder.Build(notification), notification.Receivers);
            }
        }
    }
}
