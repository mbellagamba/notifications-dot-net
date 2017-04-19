using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notifications.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}