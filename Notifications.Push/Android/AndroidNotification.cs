using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    [DataContract]
    public class AndroidNotification
    {
        /// <summary>
        /// The notification's title.
        /// </summary>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }
        /// <summary>
        /// The notification's body text.
        /// </summary>
        [DataMember(Name = "body", EmitDefaultValue = false)]
        public string Body { get; set; }
        /// <summary>
        /// The notification's channel id (new in Android O).
        /// The app must create a channel with this ID before any notification with this key is received.
        /// If you don't send this key in the request, or if the channel id provided has not yet been
        /// created by your app, FCM uses the channel id specified in your app manifest.
        /// See more at https://developer.android.com/preview/features/notification-channels.html.
        /// </summary>
        [DataMember(Name = "android_channel_id", EmitDefaultValue = false)]
        public string AndroidChannelId { get; set; }
        /// <summary>
        /// The notification's icon.
        /// Sets the notification icon to myicon for drawable resource myicon. If you don't send this key in the request, FCM displays the launcher icon specified in your app manifest.
        /// </summary>
        [DataMember(Name = "icon", EmitDefaultValue = false)]
        public string Icon { get; set; }
        /// <summary>
        /// The sound to play when the device receives the notification.
        /// Supports "default" or the filename of a sound resource bundled in the app. Sound files must reside in /res/raw/.
        /// </summary>
        [DataMember(Name = "sound", EmitDefaultValue = false)]
        public string Sound { get; set; }
        /// <summary>
        /// Identifier used to replace existing notifications in the notification drawer.
        /// If not specified, each request creates a new notification.
        /// If specified and a notification with the same tag is already being shown, the new notification replaces the existing one in the notification drawer.
        /// </summary>
        [DataMember(Name = "tag", EmitDefaultValue = false)]
        public string Tag { get; set; }
        /// <summary>
        /// The notification's icon color, expressed in #rrggbb format.
        /// </summary>
        [DataMember(Name = "color", EmitDefaultValue = false)]
        public string Color { get; set; }
        /// <summary>
        /// The action associated with a user click on the notification.
        /// If specified, an activity with a matching intent filter is launched when a user clicks on the notification.
        /// </summary>
        [DataMember(Name = "click_action", EmitDefaultValue = false)]
        public string ClickAction { get; set; }
        /// <summary>
        /// The key to the body string in the app's string resources to use to localize the body text to the user's current localization.
        /// See String Resources for more information, https://developer.android.com/guide/topics/resources/string-resource.html.
        /// </summary>
        [DataMember(Name = "body_loc_key", EmitDefaultValue = false)]
        public string BodyLocKey { get; set; }
        /// <summary>
        /// Variable string values to be used in place of the format specifiers in body_loc_key to use to localize the body text to the user's current localization.
        /// See Formatting and Styling for more information, https://developer.android.com/guide/topics/resources/string-resource.html#FormattingAndStyling.
        /// </summary>
        [DataMember(Name = "body_loc_args", EmitDefaultValue = false)]
        public string BodyLocArgs { get; set; }
        /// <summary>
        /// The key to the title string in the app's string resources to use to localize the title text to the user's current localization.
        /// See String Resources for more information, https://developer.android.com/guide/topics/resources/string-resource.html.
        /// </summary>
        [DataMember(Name = "title_loc_key", EmitDefaultValue = false)]
        public string TitleLocKey { get; set; }
        /// <summary>
        /// Variable string values to be used in place of the format specifiers in title_loc_key to use to localize the title text to the user's current localization.
        /// See Formatting and Styling for more information, https://developer.android.com/guide/topics/resources/string-resource.html#FormattingAndStyling.
        /// </summary>
        [DataMember(Name = "title_loc_args", EmitDefaultValue = false)]
        public IEnumerable<string> TitleLocArgs { get; set; }
        /// <summary>
        /// An optional custom field.
        /// </summary>
        [DataMember(Name = "info", EmitDefaultValue = false)]
        public object Info { get; set; }

        public AndroidNotification()
        {
        }

        public override string ToString()
        {
            return "{AndroidNotification(Title=" + Title + ",Body=" + Body + ")}";
        }

        public AndroidNotification(NotificationPayload payload)
        {
            Title = payload.Title;
            Body = payload.Message;
            Sound = payload.Sound;
            ClickAction = payload.Action;
            Icon = payload.Image;
            Info = GenerateInfo(payload);
        }

        private Dictionary<string, object> GenerateInfo(NotificationPayload payload)
        {
            Dictionary<string, object> info = new Dictionary<string, object>();
            if (payload.ObjectId != 0)
            {
                info.Add("objectId", payload.ObjectId);
            }
            return info.Count > 0 ? info : null;
        }
    }
}
