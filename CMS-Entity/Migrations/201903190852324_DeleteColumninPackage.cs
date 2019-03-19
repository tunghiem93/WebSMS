namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteColumninPackage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CMS_DepositPackage", "PackagePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CMS_DepositPackage", "PackagePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
