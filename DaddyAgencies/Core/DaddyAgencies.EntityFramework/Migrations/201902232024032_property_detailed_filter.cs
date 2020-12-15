namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class property_detailed_filter : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Properties", "RegionUid", "dbo.Regions");
            DropIndex("dbo.Properties", new[] { "RegionUid" });
            CreateTable(
                "dbo.Departaments",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        MapBounds = c.String(maxLength: 128),
                        MapCenter = c.String(maxLength: 128),
                        MapZoom = c.String(maxLength: 128),
                        Description = c.String(maxLength: 500),
                        RegionUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.Regions", t => t.RegionUid)
                .Index(t => t.RegionUid);
            
            AddColumn("dbo.Properties", "DepartamentUid", c => c.Guid());
            CreateIndex("dbo.Properties", "DepartamentUid");
            AddForeignKey("dbo.Properties", "DepartamentUid", "dbo.Departaments", "Uid");
            DropColumn("dbo.Inseptions", "Type");
            DropColumn("dbo.WebUsers", "FullName");
            DropColumn("dbo.Properties", "RegionUid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Properties", "RegionUid", c => c.Guid(nullable: false));
            AddColumn("dbo.WebUsers", "FullName", c => c.String());
            AddColumn("dbo.Inseptions", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.Departaments", "RegionUid", "dbo.Regions");
            DropForeignKey("dbo.Properties", "DepartamentUid", "dbo.Departaments");
            DropIndex("dbo.Properties", new[] { "DepartamentUid" });
            DropIndex("dbo.Departaments", new[] { "RegionUid" });
            DropColumn("dbo.Properties", "DepartamentUid");
            DropTable("dbo.Departaments");
            CreateIndex("dbo.Properties", "RegionUid");
            AddForeignKey("dbo.Properties", "RegionUid", "dbo.Regions", "Uid");
        }
    }
}
