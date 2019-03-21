namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableMarketing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CMS_Marketing", "CustomerId", "dbo.CMS_Customers");
            DropIndex("dbo.CMS_Marketing", new[] { "CustomerId" });
            AddColumn("dbo.CMS_Marketing", "CustomerName", c => c.String(maxLength: 250));
            AddColumn("dbo.CMS_Marketing", "SMSContent", c => c.String(maxLength: 1000));
            AddColumn("dbo.CMS_Marketing", "SendTo", c => c.String(maxLength: 60, unicode: false));
            AddColumn("dbo.CMS_Marketing", "SendFrom", c => c.String(maxLength: 60, unicode: false));
            AddColumn("dbo.CMS_Marketing", "SMSPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CMS_Marketing", "CMS_Customers_Id", c => c.String(maxLength: 60, unicode: false));
            AlterColumn("dbo.CMS_Marketing", "CustomerId", c => c.String(maxLength: 60, unicode: false));
            AlterColumn("dbo.CMS_Marketing", "TimeInput", c => c.DateTime());
            CreateIndex("dbo.CMS_Marketing", "CMS_Customers_Id");
            AddForeignKey("dbo.CMS_Marketing", "CMS_Customers_Id", "dbo.CMS_Customers", "Id");
            DropColumn("dbo.CMS_Marketing", "Phone");
            DropColumn("dbo.CMS_Marketing", "Message");
            DropColumn("dbo.CMS_Marketing", "NetworkType");
            DropColumn("dbo.CMS_Marketing", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CMS_Marketing", "Cost", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CMS_Marketing", "NetworkType", c => c.Int());
            AddColumn("dbo.CMS_Marketing", "Message", c => c.String(maxLength: 1000));
            AddColumn("dbo.CMS_Marketing", "Phone", c => c.String(maxLength: 60, unicode: false));
            DropForeignKey("dbo.CMS_Marketing", "CMS_Customers_Id", "dbo.CMS_Customers");
            DropIndex("dbo.CMS_Marketing", new[] { "CMS_Customers_Id" });
            AlterColumn("dbo.CMS_Marketing", "TimeInput", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CMS_Marketing", "CustomerId", c => c.String(nullable: false, maxLength: 60, unicode: false));
            DropColumn("dbo.CMS_Marketing", "CMS_Customers_Id");
            DropColumn("dbo.CMS_Marketing", "SMSPrice");
            DropColumn("dbo.CMS_Marketing", "SendFrom");
            DropColumn("dbo.CMS_Marketing", "SendTo");
            DropColumn("dbo.CMS_Marketing", "SMSContent");
            DropColumn("dbo.CMS_Marketing", "CustomerName");
            CreateIndex("dbo.CMS_Marketing", "CustomerId");
            AddForeignKey("dbo.CMS_Marketing", "CustomerId", "dbo.CMS_Customers", "Id", cascadeDelete: true);
        }
    }
}
