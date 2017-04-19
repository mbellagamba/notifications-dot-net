using Notifications.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    class LowPriceNotificationCondition : INotificationCondition
    {
        private const double LOW_PRICE_THRESHOLD = 0.50;
        private Notification notification;

        public LowPriceNotificationCondition(Notification notification)
        {
            this.notification = notification;
        }

        /// <summary>
        /// The notification condition is verified when the price of the product is lower than the specified threshold.
        /// </summary>
        /// <returns>true, if the product price is low.</returns>
        public bool isVerified()
        {
            return notification.Product.Price < LOW_PRICE_THRESHOLD;
        }
    }
}
