namespace Notifications.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DeviceNotification
    {
        public bool Active { get; set; }
        
        public DateTime NotifiedAt { get; set; }
        
        [Key, Column(Order = 0), ForeignKey("Device")]
        public int DeviceId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Notification")]
        public int NotificationId { get; set; }

        public virtual Device Device { get; set; }
        
        public virtual Notification Notification { get; set; }
    }
}
