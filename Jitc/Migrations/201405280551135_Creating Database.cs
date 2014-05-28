namespace Jitc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Guid(nullable: false),
                        DateToSend = c.DateTime(nullable: false),
                        TimeToSend = c.DateTime(nullable: false),
                        MailingListId = c.Int(nullable: false),
                        SendToMail = c.String(),
                        Subject = c.String(nullable: false),
                        MessageBody = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        MailRecipients_MailRecipientId = c.Int(),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.MailingLists", t => t.MailingListId, cascadeDelete: true)
                .ForeignKey("dbo.MailRecipients", t => t.MailRecipients_MailRecipientId)
                .Index(t => t.MailingListId)
                .Index(t => t.MailRecipients_MailRecipientId);
            
            CreateTable(
                "dbo.MailingLists",
                c => new
                    {
                        MailingListId = c.Int(nullable: false, identity: true),
                        MailingName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MailingListId);
            
            CreateTable(
                "dbo.MailRecipients",
                c => new
                    {
                        MailRecipientId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        Email = c.String(),
                        Company = c.String(),
                    })
                .PrimaryKey(t => t.MailRecipientId);
            
            CreateTable(
                "dbo.SentMails",
                c => new
                    {
                        MailId = c.Int(nullable: false, identity: true),
                        MailRecipientId = c.Int(nullable: false),
                        SentToMail = c.String(nullable: false),
                        SentFromMail = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        SentDate = c.DateTime(nullable: false),
                        MailOpened = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MailId)
                .ForeignKey("dbo.MailRecipients", t => t.MailRecipientId, cascadeDelete: true)
                .Index(t => t.MailRecipientId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false, identity: true),
                        MailingListID = c.Int(nullable: false),
                        MailRecipientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriptionId)
                .ForeignKey("dbo.MailingLists", t => t.MailingListID, cascadeDelete: true)
                .ForeignKey("dbo.MailRecipients", t => t.MailRecipientID, cascadeDelete: true)
                .Index(t => t.MailingListID)
                .Index(t => t.MailRecipientID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscriptions", "MailRecipientID", "dbo.MailRecipients");
            DropForeignKey("dbo.Subscriptions", "MailingListID", "dbo.MailingLists");
            DropForeignKey("dbo.Emails", "MailRecipients_MailRecipientId", "dbo.MailRecipients");
            DropForeignKey("dbo.SentMails", "MailRecipientId", "dbo.MailRecipients");
            DropForeignKey("dbo.Emails", "MailingListId", "dbo.MailingLists");
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Subscriptions", new[] { "MailRecipientID" });
            DropIndex("dbo.Subscriptions", new[] { "MailingListID" });
            DropIndex("dbo.Emails", new[] { "MailRecipients_MailRecipientId" });
            DropIndex("dbo.SentMails", new[] { "MailRecipientId" });
            DropIndex("dbo.Emails", new[] { "MailingListId" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SentMails");
            DropTable("dbo.MailRecipients");
            DropTable("dbo.MailingLists");
            DropTable("dbo.Emails");
        }
    }
}
