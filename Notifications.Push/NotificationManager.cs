using System.Collections.Generic;
using System.Linq;
using Notifications.Data;

namespace Notifications.Push
{
    public class NotificationManager
    {
        private string certificatePath;
        private string certificatePassword;
        private NotificationsModel db;

        public NotificationManager(NotificationsModel db, string certificatePath, string certificatePassword)
        {
            this.db = db;
            this.certificatePath = certificatePath;
            this.certificatePassword = certificatePassword;
        }

        public NotificationManager()
        {
            // Prefer the other constructor
            this.db = new NotificationsModel();
            this.certificatePath = "";
            this.certificatePassword = "";
        }

        /// <summary>
        /// Send a broadcast notification. Send the notification to each device in the database.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        public void Send(NotificationPayload notification)
        {
            Send(notification, db.Devices);
        }

        /// <summary>
        /// Send a notification to each device belonging to each member of the specified list.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        /// <param name="users">The list of receivers</param>
        public void Send(NotificationPayload notification, IEnumerable<User> users)
        {
            Send(notification, users.SelectMany(u => u.Devices).Distinct());
        }

        /// <summary>
        /// Send a notification to each device in the list.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        /// <param name="devices">The list of devices to which send the notification.</param>
        private void Send(NotificationPayload notification, IEnumerable<Device> devices)
        {
            INotificationService notificationService;
            foreach (Device device in devices)
            {
                notificationService = NotificationServiceFactory.Make(device.Type, certificatePath, certificatePassword);
                if (notificationService != null)
                {
                    notificationService.Send(notification, device);
                }
            }
        }

        //private static void SendAndroidNotification(string udid, string token, NOTIFICATION_TYPE eNotificationType, int iObjId, int notificationId, string message = "")
        //{

        //    string GoogleAppID = "AIzaSyDakRM-dGqP1Us-4JiF1Xsn0El6i6qXX8E";
        //    var SENDER_ID = "2523660281";
        //    WebRequest tRequest;
        //    tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        //    tRequest.Method = "post";
        //    tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
        //    tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

        //    tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        //    string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + message +
        //        "&data.title=" + message + 
        //        "&data.time=" + System.DateTime.Now.ToString() +
        //        "&data.type=" + eNotificationType.GetHashCode() +
        //        "&data.objId=" + iObjId.ToString() +
        //        "&data.notificationId=" + notificationId.ToString() +
        //        "&registration_id=" + token + "";
        //    Console.WriteLine(postData);
        //    Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //    tRequest.ContentLength = byteArray.Length;

        //    Stream dataStream = tRequest.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();

        //    WebResponse tResponse = tRequest.GetResponse();

        //    dataStream = tResponse.GetResponseStream();

        //    StreamReader tReader = new StreamReader(dataStream);

        //    String sResponseFromServer = tReader.ReadToEnd();
        //    //LogBLL.WriteInfo("NOTIFICA ANDROID", sResponseFromServer);

        //    tReader.Close();
        //    dataStream.Close();
        //    tResponse.Close();
        //}
    }
}
