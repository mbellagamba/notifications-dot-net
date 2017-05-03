using System;

namespace Notifications.Push
{
    public interface IDevice
    {
        /// <summary>
        /// The device Token used by the APNS.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// The device operating system. IOS = 0, ANDROID = 1
        /// </summary>
        int Type { get; }

        /// <summary>
        /// Return the time when the server sent the specified notification to the receiver for the last time.
        /// </summary>
        /// <param name="notification">A notification.</param>
        /// <returns>The datetime when the user was notified.</returns>
        DateTime LastNotifiedDate(INotification notification);

        /// <summary>
        /// Define if the receiver activate or not this push notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>true, if the notification is active for the receiver.</returns>
        bool IsNotificationActive(INotification notification);

        /// <summary>
        /// Set the device as notified by the notification given as parameter.
        /// </summary>
        /// <param name="notification">The notification sent.</param>
        void Notified(INotification notification);
    }
}
