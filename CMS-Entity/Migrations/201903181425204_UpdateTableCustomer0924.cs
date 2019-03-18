namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCustomer0924 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CMS_Customers", "IsVerifiedEmail", c => c.Boolean());
            AlterColumn("dbo.CMS_Customers", "IsVerifiedPhone", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CMS_Customers", "IsVerifiedPhone", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CMS_Customers", "IsVerifiedEmail", c => c.Boolean(nullable: false));
        }
    }
}
