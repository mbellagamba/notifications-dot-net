namespace Notifications.Data
{
    using System.Collections.Generic;

    public class User
    {
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
    }
}