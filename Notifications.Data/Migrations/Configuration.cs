namespace Notifications.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Notifications.Data.NotificationsModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Notifications.Data.NotificationsModel context)
        {
            //  This method will be called after migrating to the latest version.
            Group g1 = new Group() { Name = "admins" };
            Group g2 = new Group() { Name = "users" };
            User u1 = new User() { Name = "Mirco Bellagamba", Email = "mirco.bellagamba@nautes.com" };
            User u2 = new User() { Name = "Lorenzo Carducci", Email = "lorenzo.carducci@nautes.com" };
            User u3 = new User() { Name = "Riccardo Testa", Email = "riccardo.testa@nautes.com" };
            u1.Groups.Add(g1);
            u2.Groups.Add(g2);
            u3.Groups.Add(g1);
            Device d1 = new Device() { IsTablet = false, Type = 0, Owner = u1, IdentifierForVendor = "ciao1" };
            Device d2 = new Device() { IsTablet = false, Type = 0, Owner = u2, IdentifierForVendor = "ciao2" };
            Device d3 = new Device() { IsTablet = false, Type = 0, Owner = u3, IdentifierForVendor = "ciao", Token = "906ada2609ecad13f5cff1ae21d4723b559831400c9d97c0efb1b6701450cf3f" };
            Product p0 = new Product() { Code = "0000", Color = "blue", Name = "life", Type = "purchase" };
            Product p1 = new Product() { Code = "0001", Color = "blue", Name = "powers", Type = "subscription" };
            Product p2 = new Product() { Code = "0002", Color = "red", Name = "bombs", Type = "purchase" };
            Product p3 = new Product() { Code = "0003", Color = "black", Name = "shield", Type = "purchase" };
            context.Groups.AddOrUpdate(g => g.Name, g1, g2);
            context.Users.AddOrUpdate(u => u.Name, u1, u2, u3);
            context.Devices.AddOrUpdate(d => d.IdentifierForVendor, d1, d2, d3);
            context.Products.AddOrUpdate(p => p.Code, p0, p1, p2, p3);
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
