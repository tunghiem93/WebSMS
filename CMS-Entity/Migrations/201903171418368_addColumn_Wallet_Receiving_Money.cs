namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_Wallet_Receiving_Money : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Customers", "Wallet_Receiving_Money", c => c.String(maxLength: 60, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Customers", "Wallet_Receiving_Money");
        }
    }
}
