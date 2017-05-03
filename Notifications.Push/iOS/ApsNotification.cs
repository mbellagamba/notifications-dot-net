using System.Runtime.Serialization;

namespace Notifications.Push
{
    /// <summary>
    ///  For each notification, you provide a payload with app-specific information and details about how to deliver the notification to the user. 
    ///  The payload is a JSON dictionary object (as defined by RFC 4627) that you create on your server. The JSON dictionary object must contain 
    ///  an aps key, the value of which is a dictionary containing data used by the system to deliver the notification. The main contents of the 
    ///  aps dictionary determine whether the system does any of the following:
    ///  * Displays an alert message to the user
    ///  * Applies a badge to the app’s icon
    ///  * Plays a sound
    ///  * Delivers the notification silently
    ///  In addition to the aps dictionary, the JSON dictionary can include custom keys and values with your app-specific content. Custom values must
    ///  use the JSON structure and use only primitive types such as dictionary (object), array, string, number, and Boolean. Do not include customer 
    ///  information or any sensitive data in your payload unless that data is encrypted or useless outside of the context of your app. For example, 
    ///  you could include a conversation identifier that your an instant messaging app could then use to locate the corresponding user conversation. 
    ///  The data in a notification should never be destructive—that is, your app should never use a notification to delete data on the user’s device.
    ///  When using the HTTP/2 based APNS provider API, the maximum size for your JSON dictionary is 4KB. For legacy APIs, the payload size is smaller. 
    ///  For information and examples of how to create payloads, see Creating the Remote Notification Payload.
    ///  https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/CreatingtheNotificationPayload.html#//apple_ref/doc/uid/TP40008194-CH10-SW1
    /// </summary>
    [DataContract]
    public class ApsNotification
    {
        public const string DEFAULT_SOUND = "default";
        /// <summary>
        /// Include this key when you want the system to display a standard alert or a banner. 
        /// The notification settings for your app on the user’s device determine whether an alert or banner is displayed.
        /// The preferred value for this key is a dictionary, the keys for which are listed in Table 9-2. 
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/PayloadKeyReference.html#//apple_ref/doc/uid/TP40008194-CH17-SW5
        /// If you specify a string as the value of this key, that string is displayed as the message text of the alert or banner.
        /// The JSON \U notation is not supported. Put the actual UTF-8 character in the alert text instead. 
        /// </summary>
        [DataMember(Name = "alert", EmitDefaultValue = false)]
        public IOSAlert Alert { get; set; }

        /// <summary>
        /// Include this key when you want the system to modify the badge of your app icon. 
        /// If this key is not included in the dictionary, the badge is not changed. To remove the badge, set the value of this key to 0. 
        /// </summary>
        [DataMember(Name = "badge", EmitDefaultValue = false)]
        public int? Badge { get; set; }

        /// <summary>
        /// Include this key when you want the system to play a sound. The value of this key is the name of a 
        /// sound file in your app’s main bundle or in the Library/Sounds folder of your app’s data container. 
        /// If the sound file cannot be found, or if you specify `default` for the value, the system plays the default alert sound.
        /// For details about providing sound files for notifications; see Preparing Custom Alert Sounds.
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/SupportingNotificationsinYourApp.html#//apple_ref/doc/uid/TP40008194-CH4-SW10
        /// </summary>
        [DataMember(Name = "sound", EmitDefaultValue = false)]
        public string Sound { get; set; }

        /// <summary>
        /// Include this key with a value of 1 to configure a silent notification. When this key is present, 
        /// the system wakes up your app in the background and delivers the notification to its app delegate. 
        /// For information about configuring and handling silent notifications, see Configuring a Silent Notification.
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/CreatingtheNotificationPayload.html#//apple_ref/doc/uid/TP40008194-CH10-SW8
        /// </summary>
        [DataMember(Name = "content-available", EmitDefaultValue = false)]
        public int? ContentAvailable { get; set; }

        /// <summary>
        /// Provide this key with a string value that represents the notification’s type. 
        /// This value corresponds to the value in the identifier property of one of your app’s registered categories. 
        /// To learn more about using custom actions, see Configuring Categories and Actionable Notifications. 
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/SupportingNotificationsinYourApp.html#//apple_ref/doc/uid/TP40008194-CH4-SW26
        /// </summary>
        [DataMember(Name = "category", EmitDefaultValue = false)]
        public string Category { get; set; }

        /// <summary>
        /// Provide this key with a string value that represents the app-specific identifier for grouping notifications.
        /// If you provide a Notification Content app extension, you can use this value to group your notifications together.
        /// For local notifications, this key corresponds to the threadIdentifier property of the UNNotificationContent object. 
        /// </summary>
        [DataMember(Name = "thread-id", EmitDefaultValue = false)]
        public string ThreadId { get; set; }

        public override string ToString()
        {
            return "{ApsNotification(Alert=" + Alert + ",Badge=" + Badge + ",Sound=" + Sound + ",ContentAvailable=" + ContentAvailable + ")}";
        }
    }
}
