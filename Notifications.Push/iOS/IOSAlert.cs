using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Notifications.Push
{
    [DataContract]
    public class IOSAlert
    {
        /// <summary>
        /// A short string describing the purpose of the notification. Apple Watch displays this string as part of the notification interface. 
        /// This string is displayed only briefly and should be crafted so that it can be understood quickly. This key was added in iOS 8.2. 
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// The text of the alert message. 
        /// </summary>
        [DataMember(Name = "body")]
        public string Body { get; set; }

        /// <summary>
        /// The key to a title string in the Localizable.strings file for the current localization. 
        /// The key string can be formatted with %@ and %n$@ specifiers to take the variables specified in the title-loc-args array. 
        /// See Localizing the Content of Your Remote Notifications for more information. This key was added in iOS 8.2. 
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/CreatingtheNotificationPayload.html#//apple_ref/doc/uid/TP40008194-CH10-SW9
        /// </summary>
        [DataMember(Name = "title-loc-key", EmitDefaultValue = false)]
        public string TitleLocalizationKey { get; set; }

        /// <summary>
        /// Variable string values to appear in place of the format specifiers in title-loc-key. 
        /// See Localizing the Content of Your Remote Notifications for more information. This key was added in iOS 8.2. 
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/CreatingtheNotificationPayload.html#//apple_ref/doc/uid/TP40008194-CH10-SW9
        /// </summary>
        [DataMember(Name = "title-loc-args", EmitDefaultValue = false)]
        public IEnumerable<string> TitleLocalizationArgument { get; set; }

        /// <summary>
        /// If a string is specified, the system displays an alert that includes the Close and View buttons.
        /// The string is used as a key to get a localized string in the current localization to use for 
        /// the right button’s title instead of “View”. See Localizing the Content of Your Remote Notifications for more information. 
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/CreatingtheNotificationPayload.html#//apple_ref/doc/uid/TP40008194-CH10-SW9
        /// </summary>
        [DataMember(Name = "action-loc-key", EmitDefaultValue = false)]
        public string ActionLocalizationKey { get; set; }

        /// <summary>
        /// A key to an alert-message string in a Localizable.strings file for the current localization 
        /// (which is set by the user’s language preference). The key string can be formatted 
        /// with %@ and %n$@ specifiers to take the variables specified in the loc-args array. 
        /// See Localizing the Content of Your Remote Notifications for more information. 
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/CreatingtheNotificationPayload.html#//apple_ref/doc/uid/TP40008194-CH10-SW9
        /// </summary>
        [DataMember(Name = "loc-key", EmitDefaultValue = false)]
        public string LocalizationKey { get; set; }

        /// <summary>
        /// Variable string values to appear in place of the format specifiers in loc-key. 
        /// See Localizing the Content of Your Remote Notifications for more information.
        /// https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/CreatingtheNotificationPayload.html#//apple_ref/doc/uid/TP40008194-CH10-SW9
        /// </summary>
        [DataMember(Name = "loc-args", EmitDefaultValue = false)]
        public IEnumerable<string> LocalizationArguments { get; set; }

        /// <summary>
        /// The filename of an image file in the app bundle, with or without the filename extension. 
        /// The image is used as the launch image when users tap the action button or move the action slider. 
        /// If this property is not specified, the system either uses the previous snapshot, uses the image 
        /// identified by the UILaunchImageFile key in the app’s Info.plist file, or falls back to Default.png. 
        /// </summary>
        [DataMember(Name = "launch-image", EmitDefaultValue = false)]
        public string LaunchImage { get; set; }

        public override string ToString()
        {
            string launchImageDescription = LaunchImage != null ? (",LaunchImage=" + LaunchImage) : "";
            return "{IOSAlert(Title=" + Title + ",Body=" + Body + launchImageDescription + ")}";
        }
    }
}
