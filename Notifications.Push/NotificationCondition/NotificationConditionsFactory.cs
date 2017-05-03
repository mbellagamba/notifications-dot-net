using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{

    public sealed class NotificationConditionsFactory
    {
        private static readonly NotificationConditionsFactory instance = new NotificationConditionsFactory();
        private Dictionary<string, Type> conditions;

        private NotificationConditionsFactory()
        {
            conditions = new Dictionary<string, Type>();
        }

        public static NotificationConditionsFactory Instance
        {
            get { return instance; }
        }

        public void Register<Condition>(string key) where Condition : INotificationCondition
        {
            conditions.Add(key, typeof(Condition));
        }

        public INotificationCondition Create(INotification notification)
        {
            string conditionKey = notification.Condition;
            INotificationCondition condition;
            if (conditions.ContainsKey(conditionKey))
            {
                condition = (INotificationCondition)Activator.CreateInstance(conditions[conditionKey]);
            }
            else
            {
                condition = new DefaultNotificationCondition();
            }
            condition.Notification = notification;
            return condition;
        }

    }
}
