namespace Notifications.Data
{
    using Notifications.Push;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public partial class Device : IDevice
    {
        public int Id { get; set; }
        public string IdentifierForVendor { get; set; }
        public string Token { get; set; }
        public int Type { get; set; }
        public bool IsTablet { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<DeviceNotification> DeviceNotifications { get; set; }

        public DateTime LastNotifiedDate(INotification notification)
        {
            DeviceNotification deviceNotification = Find(notification);
            return deviceNotification != null ? deviceNotification.NotifiedAt : DateTime.MinValue;
        }

        public bool IsNotificationActive(INotification notification)
        {
            DeviceNotification deviceNotification = Find(notification);
            return deviceNotification != null ? deviceNotification.Active : false;
        }

        public void Notified(INotification notification)
        {
            DeviceNotification deviceNotification = Find(notification);
            if (deviceNotification != null)
            {
                deviceNotification.NotifiedAt = DateTime.UtcNow;
            }
        }

        private DeviceNotification Find(INotification notification)
        {
            return DeviceNotifications.FirstOrDefault(dn => dn.Notification.Id == notification.Id );
        }
    }
}