namespace Notifications.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Sound { get; set; }
        [Required]
        public string Event { get; set; }
        [Required]
        public string Builder { get; set; }

        public virtual Product Product { get; set; }
    }
}