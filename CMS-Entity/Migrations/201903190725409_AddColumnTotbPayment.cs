namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnTotbPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_PaymentMethod", "WalletMoney", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.CMS_PaymentMethod", "TagContent", c => c.String(maxLength: 250));
            AddColumn("dbo.CMS_PaymentMethod", "ScaleNumber", c => c.Int());
            AddColumn("dbo.CMS_PaymentMethod", "ReferenceExchange", c => c.String(maxLength: 60));
            //AddColumn("dbo.CMS_SysConfigs", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //AddColumn("dbo.CMS_SysConfigs", "ValueType", c => c.Int(nullable: false));
            AlterColumn("dbo.CMS_Customers", "SMSBalances", c => c.Int());
            AlterColumn("dbo.CMS_Customers", "IsVerifiedEmail", c => c.Boolean());
            AlterColumn("dbo.CMS_Customers", "IsVerifiedPhone", c => c.Boolean());
            //DropColumn("dbo.CMS_SysConfigs", "Rate");
            //DropColumn("dbo.CMS_SysConfigs", "RateType");
            //DropColumn("dbo.CMS_SysConfigs", "WaitingTime");
            //DropColumn("dbo.CMS_SysConfigs", "TotalCredit");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.CMS_SysConfigs", "TotalCredit", c => c.Decimal(precision: 18, scale: 2));
            //AddColumn("dbo.CMS_SysConfigs", "WaitingTime", c => c.Int(nullable: false));
            //AddColumn("dbo.CMS_SysConfigs", "RateType", c => c.Int(nullable: false));
            //AddColumn("dbo.CMS_SysConfigs", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CMS_Customers", "IsVerifiedPhone", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CMS_Customers", "IsVerifiedEmail", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CMS_Customers", "SMSBalances", c => c.Int(nullable: false));
            //DropColumn("dbo.CMS_SysConfigs", "ValueType");
            //DropColumn("dbo.CMS_SysConfigs", "Value");
            DropColumn("dbo.CMS_PaymentMethod", "ReferenceExchange");
            DropColumn("dbo.CMS_PaymentMethod", "ScaleNumber");
            DropColumn("dbo.CMS_PaymentMethod", "TagContent");
            DropColumn("dbo.CMS_PaymentMethod", "WalletMoney");
        }
    }
}
