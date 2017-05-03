using System;
using System.Collections.Generic;

namespace Notifications.Push
{
    public sealed class NotificationBuildersFactory
    {
        private static readonly NotificationBuildersFactory instance = new NotificationBuildersFactory();
        private Dictionary<string, Type> builders;

        private NotificationBuildersFactory()
        {
            builders = new Dictionary<string, Type>();
        }

        public static NotificationBuildersFactory Instance
        {
            get { return instance; }
        }

        public void Register<Builder>(string key) where Builder : INotificationBuilder
        {
            builders.Add(key, typeof(Builder));
        }

        public INotificationBuilder Create(string builderKey)
        {
            INotificationBuilder builder;
            if (builders.ContainsKey(builderKey))
            {
                builder = (INotificationBuilder)Activator.CreateInstance(builders[builderKey]);
            }
            else
            {
                builder = new DefaultNotificationBuilder();
            }
            return builder;
        }
    }
}
