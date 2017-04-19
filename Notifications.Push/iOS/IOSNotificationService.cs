using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using PushSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Data;

namespace Notifications.Push
{
    class IOSNotificationService : INotificationService
    {
        private string certificatePath;
        private string certificatePassword;

        public IOSNotificationService(string certificatePath, string certificatePassword)
        {
            this.certificatePath = certificatePath;
            this.certificatePassword = certificatePassword;
        }

        public void Send(NotificationPayload notification, Device device)
        {
            if (string.IsNullOrEmpty(device.Token)) return;

            // Configuration (NOTE: .pfx can also be used here)
            ApnsConfiguration config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, certificatePath, certificatePassword);
            ApnsServiceBroker apnsBroker = new ApnsServiceBroker(config);

            apnsBroker.OnNotificationSucceeded += this.onSuccess;
            apnsBroker.OnNotificationFailed += this.onFailure;

            apnsBroker.Start();
            IOSNotification payload = new IOSNotification(notification);
            ApnsNotification apnsNotification = new ApnsNotification
            {
                DeviceToken = device.Token,
                Payload = JObject.FromObject(payload)
            };
            Console.WriteLine(apnsNotification.ToString());
            apnsBroker.QueueNotification(apnsNotification);

            apnsBroker.Stop();
        }


        private NotificationSuccessDelegate<ApnsNotification> onSuccess = apnsNotification =>
        {
            Console.WriteLine("Notification successfully sent to " + apnsNotification.DeviceToken + " with content: " + apnsNotification + ".");
        };

        private NotificationFailureDelegate<ApnsNotification> onFailure = (apnsNotification, aggregateEx) =>
        {
            aggregateEx.Handle(ex =>
            {
                if (ex is ApnsNotificationException)
                {
                    ApnsNotificationException notificationException = (ApnsNotificationException)ex;

                    // Deal with the failed notification
                    ApnsNotification failedNotification = notificationException.Notification;
                    ApnsNotificationErrorStatusCode statusCode = notificationException.ErrorStatusCode;
                    Console.WriteLine("Notification not sent.", notificationException);
                }
                else
                {
                    // Inner exception might hold more useful information like an ApnsConnectionException     
                    Console.WriteLine("Notification not sent.", ex);
                }

                // Mark it as handled
                return true;
            });
        };
    }
}
