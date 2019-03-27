namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtableSim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_Sims",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        SimName = c.String(nullable: false, maxLength: 250),
                        SimNumber = c.String(maxLength: 60, unicode: false),
                        OperatorName = c.String(maxLength: 60),
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
            DropTable("dbo.CMS_Sims");
        }
    }
}
