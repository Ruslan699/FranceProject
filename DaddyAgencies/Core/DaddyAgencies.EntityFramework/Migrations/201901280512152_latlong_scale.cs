namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latlong_scale : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 16));
            AlterColumn("dbo.Properties", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 8));
            AlterColumn("dbo.Properties", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 8));
        }
    }
}
