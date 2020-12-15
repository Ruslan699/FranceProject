namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class region_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Regions", "PathString", c => c.String(nullable: false));
            AddColumn("dbo.Regions", "MapBounds", c => c.String(maxLength: 128));
            AddColumn("dbo.Regions", "MapCenter", c => c.String(maxLength: 128));
            AddColumn("dbo.Regions", "MapZoom", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Regions", "MapZoom");
            DropColumn("dbo.Regions", "MapCenter");
            DropColumn("dbo.Regions", "MapBounds");
            DropColumn("dbo.Regions", "PathString");
        }
    }
}
