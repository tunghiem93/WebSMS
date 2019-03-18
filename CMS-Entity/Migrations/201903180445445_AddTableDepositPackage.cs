namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDepositPackage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_DepositPackage",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 60, unicode: false),
                    PackageName = c.String(nullable: false, maxLength: 250,unicode: true ),
                    PackageSMS = c.Decimal(nullable: false),
                    PackagePrice = c.Decimal(nullable: false),
                    Discount = c.Decimal(nullable: false, precision: 18, scale:2),
                    SMSPrice = c.Decimal(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.String(maxLength: 60, unicode: false),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(maxLength: 60, unicode: false),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.CMS_DepositPackage");
        }
    }
}
