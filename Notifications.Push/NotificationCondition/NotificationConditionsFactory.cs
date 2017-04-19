using Notifications.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    enum CONDITION_TYPE
    {
        LOW_PRICE
    }

    class NotificationConditionsFactory
    {
        public static INotificationCondition Make(Notification notification)
        {
            CONDITION_TYPE conditionType;
            Enum.TryParse(notification.Condition, out conditionType);
            INotificationCondition condition;
            switch(conditionType)
            {
                case CONDITION_TYPE.LOW_PRICE:
                    condition = new LowPriceNotificationCondition(notification);
                    break;
                default:
                    condition = new DefaultNotificationCondition();
                    break;
            }
            return condition;
        }
    }
}
