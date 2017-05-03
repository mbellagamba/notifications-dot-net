namespace Notifications.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdentifierForVendor = c.String(),
                        Token = c.String(),
                        Type = c.Int(nullable: false),
                        IsTablet = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DeviceNotifications",
                c => new
                    {
                        DeviceId = c.Int(nullable: false),
                        NotificationId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        NotifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceId, t.NotificationId })
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.NotificationId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Message = c.String(),
                        Sound = c.String(),
                        Action = c.String(),
                        Condition = c.String(nullable: false),
                        Builder = c.String(nullable: false),
                        RefreshTime = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Type = c.String(),
                        Color = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Udid = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.UserId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Memberships", "UserId", "dbo.Users");
            DropForeignKey("dbo.Memberships", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Devices", "UserId", "dbo.Users");
            DropForeignKey("dbo.DeviceNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "ProductId", "dbo.Products");
            DropForeignKey("dbo.DeviceNotifications", "DeviceId", "dbo.Devices");
            DropIndex("dbo.Memberships", new[] { "UserId" });
            DropIndex("dbo.Memberships", new[] { "GroupId" });
            DropIndex("dbo.Notifications", new[] { "ProductId" });
            DropIndex("dbo.DeviceNotifications", new[] { "NotificationId" });
            DropIndex("dbo.DeviceNotifications", new[] { "DeviceId" });
            DropIndex("dbo.Devices", new[] { "UserId" });
            DropTable("dbo.Memberships");
            DropTable("dbo.Groups");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Notifications");
            DropTable("dbo.DeviceNotifications");
            DropTable("dbo.Devices");
        }
    }
}
