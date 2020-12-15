namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class property_departament_foreign_key : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Properties", new[] { "DepartamentUid" });
            AlterColumn("dbo.Properties", "DepartamentUid", c => c.Guid(nullable: false));
            CreateIndex("dbo.Properties", "DepartamentUid");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Properties", new[] { "DepartamentUid" });
            AlterColumn("dbo.Properties", "DepartamentUid", c => c.Guid());
            CreateIndex("dbo.Properties", "DepartamentUid");
        }
    }
}
