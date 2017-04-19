using Notifications.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    /// <summary>
    /// The default notification condition class represents a condition that is never verified.
    /// </summary>
    class DefaultNotificationCondition : INotificationCondition
    {
        /// <summary>
        /// The default condition is never verified and could never trigger a notification.
        /// </summary>
        /// <returns>false</returns>
        public bool isVerified()
        {
            return false;
        }
    }
}
