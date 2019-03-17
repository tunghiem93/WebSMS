namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable_CMS_CustomerActiveCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_CustomerActiveCode",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        CustomerId = c.String(nullable: false, maxLength: 60, unicode: false),
                        Code = c.String(nullable: false, maxLength: 11, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CMS_Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.CMS_Customers", "Password2", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AddColumn("dbo.CMS_Customers", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.CMS_Customers", "CompanyName");
            DropColumn("dbo.CMS_Customers", "BirthDate");
            DropColumn("dbo.CMS_Customers", "Gender");
            DropColumn("dbo.CMS_Customers", "Address");
            DropColumn("dbo.CMS_Customers", "MaritalStatus");
            DropColumn("dbo.CMS_Customers", "Street");
            DropColumn("dbo.CMS_Customers", "City");
            DropColumn("dbo.CMS_Customers", "Country");
            DropColumn("dbo.CMS_Customers", "Description");
            DropColumn("dbo.CMS_Customers", "ImageURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CMS_Customers", "ImageURL", c => c.String());
            AddColumn("dbo.CMS_Customers", "Description", c => c.String());
            AddColumn("dbo.CMS_Customers", "Country", c => c.String());
            AddColumn("dbo.CMS_Customers", "City", c => c.String());
            AddColumn("dbo.CMS_Customers", "Street", c => c.String(maxLength: 250));
            AddColumn("dbo.CMS_Customers", "MaritalStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Customers", "Address", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.CMS_Customers", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.CMS_Customers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CMS_Customers", "CompanyName", c => c.String(maxLength: 250));
            DropForeignKey("dbo.CMS_CustomerActiveCode", "CustomerId", "dbo.CMS_Customers");
            DropIndex("dbo.CMS_CustomerActiveCode", new[] { "CustomerId" });
            DropColumn("dbo.CMS_Customers", "Status");
            DropColumn("dbo.CMS_Customers", "Password2");
            DropTable("dbo.CMS_CustomerActiveCode");
        }
    }
}
