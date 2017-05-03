using System.Collections.Generic;
using System.Linq;

namespace Notifications.Push
{
    class SenderService
    {
        private string certificatePath;
        private string certificatePassword;

        public SenderService(string certificatePath, string certificatePassword)
        {
            this.certificatePath = certificatePath;
            this.certificatePassword = certificatePassword;
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
                notificationService = NotificationServiceFactory.Make(device.Type, certificatePath, certificatePassword);
                if (notificationService != null)
                {
                    notificationService.Send(payload, device);
                    device.Notified(notification);
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
