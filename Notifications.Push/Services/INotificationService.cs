using System.Collections.Generic;

namespace Notifications.Push
{
    interface INotificationService
    {
        void Send(NotificationPayload notification, IDevice device);

        void Send(NotificationPayload notification, IEnumerable<IDevice> devices);
    }
}
