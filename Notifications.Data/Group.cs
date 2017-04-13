namespace Notifications.Data
{
    using System.Collections.Generic;

    public class Group
    {
        public Group()
        {
            Members = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Members { get; set; }
    }
}