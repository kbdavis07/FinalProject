namespace Jitc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddingtoSentMailClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SentMails", "Status", c => c.String(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.SentMails", "Status");
        }
    }
}
