namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propertydocumentrowno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyDocuments", "RowNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropertyDocuments", "RowNo");
        }
    }
}
