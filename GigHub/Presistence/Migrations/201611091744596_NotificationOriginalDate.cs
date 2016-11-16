namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationOriginalDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "OriginalDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "OriginalDateTime");
        }
    }
}
