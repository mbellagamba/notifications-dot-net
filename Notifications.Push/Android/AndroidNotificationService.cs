using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    class AndroidNotificationService : INotificationService
    {
        private string senderId;
        private string authToken;
        private string packageName;

        public AndroidNotificationService(string senderId, string senderAuthToken, string optionalApplicationIdPackageName = null)
        {
            this.senderId = senderId;
            this.authToken = senderAuthToken;
            this.packageName = optionalApplicationIdPackageName;
        }

        public void Send(NotificationPayload notification, IDevice device)
        {
            // Configuration
            var config = new GcmConfiguration(senderId, authToken, packageName);
            // Make the broker use Firebase
            config.GcmUrl = "https://fcm.googleapis.com/fcm/send";

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);

            // Wire up events
            gcmBroker.OnNotificationFailed += onFailure;
            gcmBroker.OnNotificationSucceeded += onSuccess;

            var payload = new AndroidNotification(notification);

            // Start the broker
            gcmBroker.Start();
            // Queue a notification to send
            gcmBroker.QueueNotification(new GcmNotification
            {
                To = device.Token,
                Data = JObject.FromObject(payload)
            });

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            gcmBroker.Stop();
        }

        public void Send(NotificationPayload notification, IEnumerable<IDevice> devices)
        {
            // Configuration
            var config = new GcmConfiguration(senderId, authToken, packageName);
            // Make the broker use Firebase
            config.GcmUrl = "https://fcm.googleapis.com/fcm/send";

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);

            // Wire up events
            gcmBroker.OnNotificationFailed += onFailure;
            gcmBroker.OnNotificationSucceeded += onSuccess;

            var payload = new AndroidNotification(notification);

            // Make chuncks of 1000 devices that is the max quote supported
            var chuncks = devices.ToList().ChunkBy(1000);

            // Start the broker
            gcmBroker.Start();
            foreach (var chunck in chuncks)
            {
                // Queue a notification to send
                gcmBroker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = devices.Select(d => d.Token).ToList<string>(),
                    Data = JObject.FromObject(payload)
                });
            }

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            gcmBroker.Stop();

        }

        private NotificationSuccessDelegate<GcmNotification> onSuccess = (successNotification) =>
        {
            Console.WriteLine("GCM Notification Sent!");
        };

        private NotificationFailureDelegate<GcmNotification> onFailure = (failedNotification, aggregateEx) =>
        {

            aggregateEx.Handle(ex =>
            {

                // See what kind of exception it was to further diagnose
                if (ex is GcmNotificationException)
                {
                    var notificationException = (GcmNotificationException)ex;

                    // Deal with the failed notification
                    var gcmNotification = notificationException.Notification;
                    var description = notificationException.Description;

                    Console.WriteLine("GCM Notification Failed: ID=" + gcmNotification.MessageId + ", Desc=" + description);
                }
                else if (ex is GcmMulticastResultException)
                {
                    var multicastException = (GcmMulticastResultException)ex;

                    foreach (var succeededNotification in multicastException.Succeeded)
                    {
                        Console.WriteLine("GCM Notification Succeeded: ID=" + succeededNotification.MessageId);
                    }

                    foreach (var failedKvp in multicastException.Failed)
                    {
                        var n = failedKvp.Key;
                        var e = failedKvp.Value;

                        Console.WriteLine("GCM Notification Failed: ID=" + n.MessageId + ", Desc=" + e.Message);
                    }

                }
                else if (ex is DeviceSubscriptionExpiredException)
                {
                    var expiredException = (DeviceSubscriptionExpiredException)ex;

                    var oldId = expiredException.OldSubscriptionId;
                    var newId = expiredException.NewSubscriptionId;

                    Console.WriteLine("Device RegistrationId Expired: " + oldId);

                    if (!string.IsNullOrWhiteSpace(newId))
                    {
                        // If this value isn't null, our subscription changed and we should update our database
                        Console.WriteLine("Device RegistrationId Changed To: " + newId);
                    }
                }
                else if (ex is RetryAfterException)
                {
                    var retryException = (RetryAfterException)ex;
                    // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                    Console.WriteLine("GCM Rate Limited, don't send more until after " + retryException.RetryAfterUtc);
                }
                else
                {
                    Console.WriteLine("GCM Notification Failed for some unknown reason");
                }

                // Mark it as handled
                return true;
            });
        };

        //private static void SendAndroidNotification(string udid, string token, int iObjId, int notificationId, string message = "")
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
        //        "&data.type=" + 0 +
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
