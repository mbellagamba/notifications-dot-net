using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Notifications.Push
{
    /// <summary>
    /// The payload for an iOS notification. Each notification your provider server sends to the Apple 
    /// Push Notification service (APNs) includes a payload. The payload contains any custom data that 
    /// you want to send to your app and includes information about how the system should notify the user. 
    /// You construct this payload as a JSON dictionary and send it as the body content of your HTTP/2 message. 
    /// The maximum size of the payload depends on the notification you are sending:
    /// * For regular remote notifications, the maximum size is 4KB (4096 bytes)
    /// * For Voice over Internet Protocol (VoIP) notifications, the maximum size is 5KB (5120 bytes)
    ///  APNs refuses notifications whose payload exceeds the maximum allowed size.
    ///  Because the delivery of remote notifications is not guaranteed, never include sensitive data 
    ///  or data that can be retrieved by other means in your payload. Instead, use notifications to alert 
    ///  the user to new information or as a signal that your app has data waiting for it. For example, 
    ///  an email app could use remote notifications to badge the app’s icon or to alert the user that 
    ///  new email is available in a specific account, as opposed to sending the contents of email messages 
    ///  directly. Upon receiving the notification, the app should open a direct connection to your 
    ///  email server to retrieve the email messages. 
    ///  
    /// See more information at Creating the Remote Notification Payload.
    /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/CreatingtheNotificationPayload.html
    /// </summary>
    [DataContract]
    public class IOSNotification
    {
        /// <summary>
        /// The most important part of the payload is the aps dictionary, which contains 
        /// Apple-defined keys and is used to determine how the system receiving the notification should alert the user, if at all.
        /// </summary>
        [DataMember(Name = "aps")]
        public ApsNotification Aps { get; set; }

        /// <summary>
        /// A custom object which could contains custom information. The notification JSON could include any keys
        /// but in the server side are structured into a single object key named 'info'. Keep in mind that the 
        /// maximum notification size is 4KB (4096 bytes)
        /// </summary>
        [DataMember(Name = "info", EmitDefaultValue = false)]
        public object Info { get; set; }

        public override string ToString()
        {
            return "{IOSNotification(Aps=" + Aps + (Info != null ? (",Info=" + Info) : "") + ")}";
        }

        public IOSNotification()
        {
        }

        public IOSNotification(NotificationPayload notification)
        {
            IOSAlert alert = new IOSAlert()
            {
                Title = notification.Title,
                Body = notification.Message
            };
            ApsNotification aps = new ApsNotification()
            {
                Alert = alert,
                Sound = string.IsNullOrEmpty(notification.Sound) ? ApsNotification.DEFAULT_SOUND : notification.Sound
            };
            this.Aps = aps;
            var info = GenerateInfo(notification);
            if (info != null)
            {
                this.Info = info;
            }
        }

        private Dictionary<string, object> GenerateInfo(NotificationPayload payload)
        {
            Dictionary<string, object> info = new Dictionary<string, object>();
            if (payload.ObjectId != 0)
            {
                info.Add("objectId", payload.ObjectId);
            }
            if (!string.IsNullOrEmpty(payload.Action))
            {
                info.Add("action", payload.Action);
            }
            return info.Count > 0 ? info : null;
        }

    }
}
