namespace Notifications.Push
{
    class DefaultNotificationBuilder : INotificationBuilder
    {
        public NotificationPayload Build(INotification notification)
        {
            NotificationPayload payload = new NotificationPayload()
            {
                Title = notification.Title,
                Message = notification.Message,
                ObjectId = notification.Object.Id
            };

            if (!string.IsNullOrEmpty(notification.Sound))
            {
                payload.Sound = notification.Sound;
            }

            if (!string.IsNullOrEmpty(notification.Action))
            {
                payload.Action = notification.Action;
            }
            return payload;
        }
    }
}
