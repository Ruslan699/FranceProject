namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inseption_type : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "PropertyUid", "dbo.Properties");
            DropForeignKey("dbo.Properties", "RegionUid", "dbo.Regions");
            DropForeignKey("dbo.Inseptions", "PropertyUid", "dbo.Properties");
            AddColumn("dbo.Inseptions", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Inseptions", "Description", c => c.String(maxLength: 1000));
            AddColumn("dbo.Inseptions", "RegionUid", c => c.Guid(nullable: false));
            CreateIndex("dbo.Inseptions", "RegionUid");
            AddForeignKey("dbo.Inseptions", "RegionUid", "dbo.Regions", "Uid");
            AddForeignKey("dbo.Documents", "PropertyUid", "dbo.Properties", "Uid");
            AddForeignKey("dbo.Properties", "RegionUid", "dbo.Regions", "Uid");
            AddForeignKey("dbo.Inseptions", "PropertyUid", "dbo.Properties", "Uid");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inseptions", "PropertyUid", "dbo.Properties");
            DropForeignKey("dbo.Properties", "RegionUid", "dbo.Regions");
            DropForeignKey("dbo.Documents", "PropertyUid", "dbo.Properties");
            DropForeignKey("dbo.Inseptions", "RegionUid", "dbo.Regions");
            DropIndex("dbo.Inseptions", new[] { "RegionUid" });
            DropColumn("dbo.Inseptions", "RegionUid");
            DropColumn("dbo.Inseptions", "Description");
            DropColumn("dbo.Inseptions", "Type");
            AddForeignKey("dbo.Inseptions", "PropertyUid", "dbo.Properties", "Uid", cascadeDelete: true);
            AddForeignKey("dbo.Properties", "RegionUid", "dbo.Regions", "Uid", cascadeDelete: true);
            AddForeignKey("dbo.Documents", "PropertyUid", "dbo.Properties", "Uid", cascadeDelete: true);
        }
    }
}
