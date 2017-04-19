using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Data;

namespace Notifications.Push
{
    interface INotificationService
    {
        void Send(NotificationPayload notification, Device device);
    }
}
