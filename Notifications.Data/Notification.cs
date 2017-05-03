﻿namespace Notifications.Data
{
    using Notifications.Push;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

    public class Notification : INotification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Notification()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Sound { get; set; }
        public string Action { get; set; }
        [Required]
        public string Condition { get; set; }
        [Required]
        public string Builder { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public INotificationObject Object
        {
            get { return Product; }
        }

        public IEnumerable<IReceiver> Receivers
        {
            get { return Users as IEnumerable<IReceiver>; }
        }
    }
}