namespace Notifications.Push
{
    public interface INotificationCondition
    {
        INotification Notification { get; set; }
        /// <summary>
        /// Should check if an event has happened.
        /// </summary>
        /// <returns>true, if the event is verified.</returns>
        bool isVerified();
    }
}
