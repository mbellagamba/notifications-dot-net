namespace Notifications.Push
{
    /// <summary>
    /// The default notification condition class represents a condition that is never verified.
    /// </summary>
    class DefaultNotificationCondition : INotificationCondition
    {
        public INotification Notification { get; set; }
        /// <summary>
        /// The default condition is never verified and could never trigger a notification.
        /// </summary>
        /// <returns>false</returns>
        public bool isVerified()
        {
            return false;
        }
    }
}
