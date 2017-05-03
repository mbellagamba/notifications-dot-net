using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    public class NotificationTrigger
    {
        private NotificationsSender sender;
        private IEnumerable<INotification> notifications;

        public NotificationTrigger(NotificationsSender sender, IEnumerable<INotification> notifications)
        {
            this.sender = sender;
            this.notifications = notifications;
        }

        public void TriggerIfNeeded()
        {
            NotificationConditionsFactory conditionsFactory = NotificationConditionsFactory.Instance;
            NotificationBuildersFactory buildersFactory = NotificationBuildersFactory.Instance;
            IEnumerable<INotification> triggeredNotifications = this.notifications
                .Where(n =>
                {
                    INotificationCondition condition = conditionsFactory.Create(n);
                    return condition.isVerified();
                });
            foreach (INotification notification in triggeredNotifications)
            {
                INotificationBuilder builder = buildersFactory.Create(notification.Builder);
                sender.Send(builder.Build(notification), notification.Receivers);
            }
        }
    }
}
