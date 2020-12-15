namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class property_room_count : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Properties", new[] { "DepartamentUid" });
            RenameColumn(table: "dbo.Properties", name: "DepartamentUid", newName: "DepartamentEntity_Uid");
            AddColumn("dbo.Properties", "RoomsCount", c => c.Int());
            AddColumn("dbo.Properties", "PostalCodeUid", c => c.Guid());
            AlterColumn("dbo.Properties", "DepartamentEntity_Uid", c => c.Guid());
            CreateIndex("dbo.Properties", "PostalCodeUid");
            CreateIndex("dbo.Properties", "DepartamentEntity_Uid");
            AddForeignKey("dbo.Properties", "PostalCodeUid", "dbo.PostalCodes", "Uid");
            DropColumn("dbo.Properties", "PostalCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Properties", "PostalCode", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Properties", "PostalCodeUid", "dbo.PostalCodes");
            DropIndex("dbo.Properties", new[] { "DepartamentEntity_Uid" });
            DropIndex("dbo.Properties", new[] { "PostalCodeUid" });
            AlterColumn("dbo.Properties", "DepartamentEntity_Uid", c => c.Guid(nullable: false));
            DropColumn("dbo.Properties", "PostalCodeUid");
            DropColumn("dbo.Properties", "RoomsCount");
            RenameColumn(table: "dbo.Properties", name: "DepartamentEntity_Uid", newName: "DepartamentUid");
            CreateIndex("dbo.Properties", "DepartamentUid");
        }
    }
}
