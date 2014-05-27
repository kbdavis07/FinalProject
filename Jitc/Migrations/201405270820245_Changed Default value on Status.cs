namespace Jitc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ChangedDefaultvalueonStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SentMails", "MailOpened", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.SentMails", "MailOpened");
        }
    }
}
