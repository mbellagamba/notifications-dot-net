using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    public class PushService
    {
        private SenderService sender;
        private TriggerService trigger;
        private bool isInit = false;

        private static readonly PushService instance = new PushService();

        private PushService() { }

        public static PushService Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Initialize the service.
        /// </summary>
        /// <param name="certificatePath">The path to the certificate.</param>
        /// <param name="certificatePassword">The certificate password.</param>
        /// <param name="notifications">The list of notifications.</param>
        public void Init(string certificatePath, string certificatePassword, string senderId, string senderAuthToken, IEnumerable<INotification> notifications)
        {
            sender = new SenderService(certificatePath, certificatePassword, senderId, senderAuthToken);
            trigger = new TriggerService(notifications);
            isInit = true;
        }

        /// <summary>
        /// Push notifications to each valid device. The <code>PushService</code> should be initialized before calling the push method.
        /// Remember to save changes to your persistance layer if needed.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the <code>PushService</code> is not initialized.</exception>
        public void Push()
        {
            if (!isInit) throw new InvalidOperationException("Could not push notification if the push service was not initialized. Call the method 'Init' before 'Push'.");

            foreach (INotification notification in trigger.Triggered())
            {
                // Get each notifiable devicesthat satisfies both those conditions:
                // 1. The notification is active on the device (the user does not disabled this push notification);
                // 2. The notification is not already sent to the device in the refresh period.
                IEnumerable<IDevice> devices = notification.NotifiableDevices.Where(d => d.IsNotificationActive(notification) && d.NotNotifiedInPeriod(notification));
                sender.Send(notification, devices);
            }
        }

        /// <exception cref="InvalidOperationException">Thrown if the <code>PushService</code> is not initialized.</exception>
        public void Push(INotification notification, IEnumerable<IDevice> devices)
        {
            if (!isInit) throw new InvalidOperationException("Could not push notification if the push service was not initialized. Call the method 'Init' before 'Push'.");
            sender.Send(notification, devices);
        }

    }
}
