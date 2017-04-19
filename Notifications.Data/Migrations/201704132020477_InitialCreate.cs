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
            
            CreateTable(
                "dbo.NotificationsReceivers",
                c => new
                    {
                        NotificationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NotificationId, t.UserId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationsReceivers", "UserId", "dbo.Users");
            DropForeignKey("dbo.NotificationsReceivers", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Memberships", "UserId", "dbo.Users");
            DropForeignKey("dbo.Memberships", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Devices", "UserId", "dbo.Users");
            DropIndex("dbo.NotificationsReceivers", new[] { "UserId" });
            DropIndex("dbo.NotificationsReceivers", new[] { "NotificationId" });
            DropIndex("dbo.Memberships", new[] { "UserId" });
            DropIndex("dbo.Memberships", new[] { "GroupId" });
            DropIndex("dbo.Notifications", new[] { "ProductId" });
            DropIndex("dbo.Devices", new[] { "UserId" });
            DropTable("dbo.NotificationsReceivers");
            DropTable("dbo.Memberships");
            DropTable("dbo.Products");
            DropTable("dbo.Notifications");
            DropTable("dbo.Groups");
            DropTable("dbo.Users");
            DropTable("dbo.Devices");
        }
    }
}
