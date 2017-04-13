using Notifications.Data;
using Notifications.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string certificatesFolder = System.Configuration.ConfigurationManager.AppSettings["certificate_path"];
            string certificatesPassword = System.Configuration.ConfigurationManager.AppSettings["certificate_password"];
            NotificationPayload n = new NotificationPayload()
            {
                Title = "Test notification",
                Message = "This is just a test notification.",
                ObjectId = 1,
                Action = ""
            };
            using (var db = new NotificationsModel())
            {
                User user = db.Users.FirstOrDefault(u => u.Name.ToLower().Contains("riccardo"));
                if (user != null)
                {
                    NotificationManager notificationManager = new NotificationManager(db, certificatesFolder, certificatesPassword);
                    notificationManager.Send(n, new User[] { user });
                }
            }
            Console.ReadKey();
        }
    }
}
