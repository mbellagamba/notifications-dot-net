using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    interface INotificationBuilder
    {
        NotificationPayload Build(Notifications.Data.Notification notification);
    }
}
