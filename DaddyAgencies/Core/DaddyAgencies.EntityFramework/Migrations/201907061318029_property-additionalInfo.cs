namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propertyadditionalInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "Ges", c => c.String());
            AddColumn("dbo.Properties", "EnergyClass", c => c.String());
            AddColumn("dbo.Properties", "Status", c => c.String());
            AddColumn("dbo.Properties", "Parking", c => c.String());
            AddColumn("dbo.Properties", "OwnerName", c => c.String());
            AddColumn("dbo.Properties", "OwnerEmail", c => c.String());
            AddColumn("dbo.Properties", "OwnerPhone", c => c.String());
            AddColumn("dbo.Properties", "OwnerNote", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "OwnerNote");
            DropColumn("dbo.Properties", "OwnerPhone");
            DropColumn("dbo.Properties", "OwnerEmail");
            DropColumn("dbo.Properties", "OwnerName");
            DropColumn("dbo.Properties", "Parking");
            DropColumn("dbo.Properties", "Status");
            DropColumn("dbo.Properties", "EnergyClass");
            DropColumn("dbo.Properties", "Ges");
        }
    }
}
