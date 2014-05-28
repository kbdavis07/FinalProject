namespace Jitc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailModelUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emails", "Recipient_MailRecipientId", "dbo.MailRecipients");
            DropIndex("dbo.Emails", new[] { "Recipient_MailRecipientId" });
            AddColumn("dbo.Emails", "MailRecipients_MailRecipientId", c => c.Int());
            CreateIndex("dbo.Emails", "MailRecipients_MailRecipientId");
            AddForeignKey("dbo.Emails", "MailRecipients_MailRecipientId", "dbo.MailRecipients", "MailRecipientId");
            DropColumn("dbo.Emails", "Recipient_MailRecipientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emails", "Recipient_MailRecipientId", c => c.Int());
            DropForeignKey("dbo.Emails", "MailRecipients_MailRecipientId", "dbo.MailRecipients");
            DropIndex("dbo.Emails", new[] { "MailRecipients_MailRecipientId" });
            DropColumn("dbo.Emails", "MailRecipients_MailRecipientId");
            CreateIndex("dbo.Emails", "Recipient_MailRecipientId");
            AddForeignKey("dbo.Emails", "Recipient_MailRecipientId", "dbo.MailRecipients", "MailRecipientId");
        }
    }
}
