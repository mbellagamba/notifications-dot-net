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
            using (var db = new NotificationsModel())
            {
                Product p0 = db.Products.Find(1);
                p0.Price = 0.49;
                db.SaveChanges();
                Sender sender = new Sender(db, certificatesFolder, certificatesPassword);
                NotificationTrigger trigger = new NotificationTrigger(sender, db.Notifications);
                trigger.TriggerIfNeeded();

                p0.Price = 0.99;
                db.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}
