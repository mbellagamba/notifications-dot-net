namespace Notifications.Data
{
    using Notifications.Push;
    using System.Collections.Generic;

    public class User : IReceiver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Groups = new HashSet<Group>();
            Devices = new HashSet<Device>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Udid { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        public IEnumerable<IDevice> NotificationDevices
        {
            get { return Devices as IEnumerable<IDevice>; }
        }
    }
}