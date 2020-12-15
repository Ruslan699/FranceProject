namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Regions", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Regions", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Regions", "Description", c => c.String());
            AlterColumn("dbo.Regions", "Name", c => c.String());
        }
    }
}
