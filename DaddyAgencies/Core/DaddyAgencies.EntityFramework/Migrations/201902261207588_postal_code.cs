namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postal_code : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostalCodes",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        MapBounds = c.String(maxLength: 128),
                        MapCenter = c.String(maxLength: 128),
                        MapZoom = c.String(maxLength: 128),
                        Description = c.String(maxLength: 500),
                        DepartamentUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.Departaments", t => t.DepartamentUid)
                .Index(t => t.DepartamentUid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostalCodes", "DepartamentUid", "dbo.Departaments");
            DropIndex("dbo.PostalCodes", new[] { "DepartamentUid" });
            DropTable("dbo.PostalCodes");
        }
    }
}
