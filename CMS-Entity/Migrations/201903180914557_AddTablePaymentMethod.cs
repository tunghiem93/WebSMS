namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablePaymentMethod : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_PaymentMethod",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        PaymentName = c.String(nullable: false, maxLength: 250),
                        PaymentType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.CMS_SysConfigs", "TotalCredit", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.CMS_SysConfigs", "TotalCredit");
            DropTable("dbo.CMS_PaymentMethod");
        }
    }
}
