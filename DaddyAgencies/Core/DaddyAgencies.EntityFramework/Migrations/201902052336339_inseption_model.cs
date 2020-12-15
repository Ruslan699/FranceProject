namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inseption_model : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Inseptions", new[] { "PropertyUid" });
            AlterColumn("dbo.Inseptions", "PropertyUid", c => c.Guid());
            CreateIndex("dbo.Inseptions", "PropertyUid");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Inseptions", new[] { "PropertyUid" });
            AlterColumn("dbo.Inseptions", "PropertyUid", c => c.Guid(nullable: false));
            CreateIndex("dbo.Inseptions", "PropertyUid");
        }
    }
}
