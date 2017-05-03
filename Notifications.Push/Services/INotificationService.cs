namespace Notifications.Push
{
    interface INotificationService
    {
        void Send(NotificationPayload notification, IDevice device);
    }
}
