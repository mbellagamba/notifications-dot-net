using Notifications.Push;
namespace Notifications.App
{
    class ProductNotificationBuilder : INotificationBuilder
    {
        private const string NAME_PLACEHOLDER = "{{product_name}}";

        public NotificationPayload Build(INotification notification)
        {
            string title = notification.Title.Replace(NAME_PLACEHOLDER, notification.Object.Name);
            NotificationPayload payload = new NotificationPayload()
            {
                Title = title,
                Message = notification.Message,
                Image = notification.Image,
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
