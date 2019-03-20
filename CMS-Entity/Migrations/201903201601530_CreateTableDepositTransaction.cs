namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableDepositTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_API",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        LinkAPI = c.String(nullable: false, maxLength: 200),
                        APIName = c.String(nullable: false, maxLength: 200),
                        APIType = c.Int(),
                        Description = c.String(storeType: "ntext"),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_Marketing",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        Phone = c.String(maxLength: 60, unicode: false),
                        Message = c.String(maxLength: 1000),
                        NetworkType = c.Int(),
                        SMSType = c.Int(),
                        RunTime = c.Decimal(precision: 18, scale: 2),
                        CustomerId = c.String(nullable: false, maxLength: 60, unicode: false),
                        Cost = c.Decimal(precision: 18, scale: 2),
                        TimeInput = c.DateTime(nullable: false),
                        OperatorName = c.String(maxLength: 100),
                        Status = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CMS_Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CMS_DepositTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        CustomerId = c.String(nullable: false, maxLength: 60, unicode: false),
                        CustomerName = c.String(maxLength: 250),
                        WalletMoney = c.String(maxLength: 60, unicode: false),
                        PackageId = c.String(maxLength: 60, unicode: false),
                        PackageName = c.String(maxLength: 250),
                        PackageSMS = c.Decimal(precision: 18, scale: 2),
                        SMSPrice = c.Decimal(precision: 18, scale: 2),
                        ExchangeRate = c.Decimal(precision: 18, scale: 2),
                        PackagePrice = c.Decimal(precision: 18, scale: 2),
                        PaymentMethodId = c.String(maxLength: 60, unicode: false),
                        PaymentMethodName = c.String(maxLength: 250),
                        PayCoin = c.Decimal(precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CMS_Marketing", "CustomerId", "dbo.CMS_Customers");
            DropIndex("dbo.CMS_Marketing", new[] { "CustomerId" });
            DropTable("dbo.CMS_DepositTransactions");
            DropTable("dbo.CMS_Marketing");
            DropTable("dbo.CMS_API");
        }
    }
}
