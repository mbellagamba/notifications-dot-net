using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    interface INotificationCondition
    {
        /// <summary>
        /// Should check if an event has happened.
        /// </summary>
        /// <returns>true, if the event is verified.</returns>
        bool isVerified();
    }
}
