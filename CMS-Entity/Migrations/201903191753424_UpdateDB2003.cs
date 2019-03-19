namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB2003 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_Marketing",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        Phone = c.String(),
                        Message = c.String(),
                        NetworkType = c.Int(nullable: false),
                        SMSType = c.Int(nullable: false),
                        RunTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.String(nullable: false, maxLength: 60, unicode: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeInput = c.DateTime(nullable: false),
                        OperatorName = c.String(),
                        Status = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CMS_Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.CMS_PaymentMethod", "WalletMoney", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.CMS_PaymentMethod", "TagContent", c => c.String(maxLength: 250));
            AddColumn("dbo.CMS_PaymentMethod", "ScaleNumber", c => c.Int());
            AddColumn("dbo.CMS_PaymentMethod", "ReferenceExchange", c => c.String(maxLength: 60));
            DropColumn("dbo.CMS_DepositPackage", "PackagePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CMS_DepositPackage", "PackagePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.CMS_Marketing", "CustomerId", "dbo.CMS_Customers");
            DropIndex("dbo.CMS_Marketing", new[] { "CustomerId" });
            DropColumn("dbo.CMS_PaymentMethod", "ReferenceExchange");
            DropColumn("dbo.CMS_PaymentMethod", "ScaleNumber");
            DropColumn("dbo.CMS_PaymentMethod", "TagContent");
            DropColumn("dbo.CMS_PaymentMethod", "WalletMoney");
            DropTable("dbo.CMS_Marketing");
        }
    }
}
