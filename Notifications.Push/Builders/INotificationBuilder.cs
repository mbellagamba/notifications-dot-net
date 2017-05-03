namespace Notifications.Push
{
    public interface INotificationBuilder
    {
        NotificationPayload Build(INotification notification);
    }
}
