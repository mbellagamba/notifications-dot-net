using Notifications.Data;
using Notifications.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.App
{
    class LowPriceNotificationCondition : INotificationCondition
    {
        private const double LOW_PRICE_THRESHOLD = 0.50;
        private Notification notification;

        /// <summary>
        /// The notification condition is verified when the price of the product is lower than the specified threshold.
        /// </summary>
        /// <returns>true, if the product price is low.</returns>
        public bool isVerified()
        {
            if (notification == null) return false;
            return notification.Product.Price < LOW_PRICE_THRESHOLD;
        }

        public INotification Notification
        {
            get
            {
                return notification;
            }
            set
            {
                notification = value is Notification ? (Notification)value : null;
            }
        }
    }
}
