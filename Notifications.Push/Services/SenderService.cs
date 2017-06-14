using System.Collections.Generic;
using System.Linq;

namespace Notifications.Push
{
    class SenderService
    {
        private string certificatePath;
        private string certificatePassword;
        private string senderId;
        private string senderAuthToken;

        public SenderService(string certificatePath, string certificatePassword, string senderId, string senderAuthToken)
        {
            this.certificatePath = certificatePath;
            this.certificatePassword = certificatePassword;
            this.senderId = senderId;
            this.senderAuthToken = senderAuthToken;
        }

        /// <summary>
        /// Send a notification to each device in the list.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        /// <param name="devices">The list of devices to which send the notification.</param>
        public void Send(INotification notification, IEnumerable<IDevice> devices)
        {
            NotificationBuildersFactory buildersFactory = NotificationBuildersFactory.Instance;
            INotificationBuilder builder = buildersFactory.Create(notification.Builder);
            NotificationPayload payload = builder.Build(notification);
            INotificationService notificationService;
            foreach (IDevice device in devices)
            {
                notificationService = NotificationServiceFactory.Make(device.Type, certificatePath, certificatePassword, senderId, senderAuthToken);
                if (notificationService != null)
                {
                    notificationService.Send(payload, device);
                    device.Notified(notification);
                }
            }
        }
    }
}
