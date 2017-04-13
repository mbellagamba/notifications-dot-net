using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Push
{
    public class NotificationPayload
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public int ObjectId { get; set; }

        public override string ToString()
        {
            return "{NotificationPayload(Id=" + Id + ",Title=" + Title + ",Message=" + Message + ",Action=" + Action + ",ObjectId=" + ObjectId + ")}";
        }
    }
}
