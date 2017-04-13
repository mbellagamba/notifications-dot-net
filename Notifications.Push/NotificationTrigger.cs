using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Data;

namespace Notifications.Push
{
    class NotificationTrigger
    {
        private NotificationManager manager;
        public INotificationCondition Condition { get; set; }
        public IEnumerable<User> Receivers { get; set; }

        public NotificationTrigger(NotificationManager manager)
        {
            this.manager = manager;
        }

        public void SendIfNeeded()
        {
            if(Condition != null && Condition.isVerified())
            {
                //manager.Send()
            }
        }
    }
}
