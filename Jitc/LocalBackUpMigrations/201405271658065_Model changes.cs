namespace Jitc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelchanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MailingLists", "MailRecipientId", "dbo.MailRecipients");
            DropIndex("dbo.MailingLists", new[] { "MailRecipientId" });
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
            
            DropColumn("dbo.MailingLists", "MailRecipientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MailingLists", "MailRecipientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Subscriptions", "MailRecipientID", "dbo.MailRecipients");
            DropForeignKey("dbo.Subscriptions", "MailingListID", "dbo.MailingLists");
            DropIndex("dbo.Subscriptions", new[] { "MailRecipientID" });
            DropIndex("dbo.Subscriptions", new[] { "MailingListID" });
            DropTable("dbo.Subscriptions");
            CreateIndex("dbo.MailingLists", "MailRecipientId");
            AddForeignKey("dbo.MailingLists", "MailRecipientId", "dbo.MailRecipients", "MailRecipientId", cascadeDelete: true);
        }
    }
}
