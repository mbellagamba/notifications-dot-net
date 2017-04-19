namespace Notifications.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class NotificationsModel : DbContext
    {
        public NotificationsModel()
            : base("name=NotificationsModel")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Members)
                .WithMany(u => u.Groups)
                .Map(m => m.MapLeftKey("GroupId").MapRightKey("UserId").ToTable("Memberships"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Devices)
                .WithRequired(d => d.Owner)
                .Map(m => m.MapKey("UserId"));

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Notifications)
                .WithRequired(n => n.Product)
                .Map(m => m.MapKey("ProductId"));

            modelBuilder.Entity<Notification>()
                .HasMany(n => n.Receivers)
                .WithMany(u => u.Notifications)
                .Map(m => m.MapLeftKey("NotificationId").MapRightKey("UserId").ToTable("NotificationsReceivers"));
        }
    }
}