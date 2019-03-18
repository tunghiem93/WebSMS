namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTableCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Customers", "CreditNumber", c => c.String(maxLength: 60, unicode: false));
            AddColumn("dbo.CMS_Customers", "SMSBalances", c => c.Int(nullable: false));
            AddColumn("dbo.CMS_Customers", "APIKey", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.CMS_Customers", "APIPass", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.CMS_Customers", "IsVerifiedEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Customers", "IsVerifiedPhone", c => c.Boolean(nullable: false));
            DropColumn("dbo.CMS_Customers", "Wallet_Receiving_Money");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CMS_Customers", "Wallet_Receiving_Money", c => c.String(maxLength: 60, unicode: false));
            DropColumn("dbo.CMS_Customers", "IsVerifiedPhone");
            DropColumn("dbo.CMS_Customers", "IsVerifiedEmail");
            DropColumn("dbo.CMS_Customers", "APIPass");
            DropColumn("dbo.CMS_Customers", "APIKey");
            DropColumn("dbo.CMS_Customers", "SMSBalances");
            DropColumn("dbo.CMS_Customers", "CreditNumber");
        }
    }
}
