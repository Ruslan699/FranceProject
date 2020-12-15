namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class region_property_init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 500),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 8),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 8),
                        PostalCode = c.String(maxLength: 50),
                        Address = c.String(maxLength: 500),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.Binary(),
                        RegionUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.Regions", t => t.RegionUid, cascadeDelete: true)
                .Index(t => t.RegionUid);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 500),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "RegionUid", "dbo.Regions");
            DropIndex("dbo.Properties", new[] { "RegionUid" });
            DropTable("dbo.Regions");
            DropTable("dbo.Properties");
        }
    }
}
