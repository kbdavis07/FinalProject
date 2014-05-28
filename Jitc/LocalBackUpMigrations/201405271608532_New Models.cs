namespace Jitc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModels : DbMigration
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
                        Recipient_MailRecipientId = c.Int(),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.MailingLists", t => t.MailingListId, cascadeDelete: true)
                .ForeignKey("dbo.MailRecipients", t => t.Recipient_MailRecipientId)
                .Index(t => t.MailingListId)
                .Index(t => t.Recipient_MailRecipientId);
            
            CreateTable(
                "dbo.MailingLists",
                c => new
                    {
                        MailingListId = c.Int(nullable: false, identity: true),
                        MailingName = c.String(nullable: false),
                        MailRecipientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MailingListId)
                .ForeignKey("dbo.MailRecipients", t => t.MailRecipientId, cascadeDelete: true)
                .Index(t => t.MailRecipientId);
         
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "Recipient_MailRecipientId", "dbo.MailRecipients");
            DropForeignKey("dbo.Emails", "MailingListId", "dbo.MailingLists");
            DropForeignKey("dbo.MailingLists", "MailRecipientId", "dbo.MailRecipients");
            DropIndex("dbo.Emails", new[] { "Recipient_MailRecipientId" });
            DropIndex("dbo.Emails", new[] { "MailingListId" });
            DropIndex("dbo.MailingLists", new[] { "MailRecipientId" });
            DropTable("dbo.MailingLists");
            DropTable("dbo.Emails");
        }
    }
}
