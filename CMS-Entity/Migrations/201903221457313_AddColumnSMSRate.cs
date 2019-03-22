namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnSMSRate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Marketing", "SMSRate", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Marketing", "SMSRate");
        }
    }
}
