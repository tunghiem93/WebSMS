namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnMemberID : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_GSM",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        GSMName = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_SimOperator",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        HeaderPhone = c.String(nullable: false, maxLength: 4, unicode: false),
                        OperaterName = c.String(nullable: false, maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CMS_Customers", "MemberID", c => c.String(maxLength: 100, unicode: false));
            //AlterColumn("dbo.CMS_PaymentMethod", "ReferenceExchange", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.CMS_PaymentMethod", "ReferenceExchange", c => c.String(maxLength: 60));
            DropColumn("dbo.CMS_Customers", "MemberID");
            DropTable("dbo.CMS_SimOperator");
            DropTable("dbo.CMS_GSM");
        }
    }
}
