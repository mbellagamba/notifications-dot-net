using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    class TriggerService
    {
        //private SenderService sender;
        private IEnumerable<INotification> notifications;

        public TriggerService(/*SenderService sender,*/ IEnumerable<INotification> notifications)
        {
            //this.sender = sender;
            this.notifications = notifications;
        }

        public IEnumerable<INotification> Triggered()
        {
            NotificationConditionsFactory conditionsFactory = NotificationConditionsFactory.Instance;
            return this.notifications.Where(n =>
                {
                    INotificationCondition condition = conditionsFactory.Create(n);
                    return condition.isVerified();
                });
            //foreach (INotification notification in triggeredNotifications)
            //{
            //    IEnumerable<IDevice> devices = ReceiversFilterService.ActiveDevices(notification, notification.NotifiableDevices);
            //    sender.Send(notification, devices);
            //}
        }
    }
}
