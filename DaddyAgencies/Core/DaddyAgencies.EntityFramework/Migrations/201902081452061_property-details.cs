namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propertydetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropertyAdditionalElements",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        Availability = c.Boolean(nullable: false),
                        PropertyUid = c.Guid(nullable: false),
                        AdditionalElementUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.AdditionalElements", t => t.AdditionalElementUid)
                .ForeignKey("dbo.Properties", t => t.PropertyUid)
                .Index(t => t.PropertyUid)
                .Index(t => t.AdditionalElementUid);
            
            CreateTable(
                "dbo.AdditionalElements",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Icon = c.Binary(),
                        Description = c.String(maxLength: 500),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid);
            
            CreateTable(
                "dbo.PropertyFloors",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Number = c.Int(nullable: false),
                        FloorTypeUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                        PropertyEntity_Uid = c.Guid(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.PropertyFloorTypes", t => t.FloorTypeUid)
                .ForeignKey("dbo.Properties", t => t.PropertyEntity_Uid)
                .Index(t => t.FloorTypeUid)
                .Index(t => t.PropertyEntity_Uid);
            
            CreateTable(
                "dbo.PropertyFloorTypes",
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
            
            AddColumn("dbo.Properties", "Condition", c => c.Int(nullable: false));
            AddColumn("dbo.Properties", "PropertyType", c => c.Int(nullable: false));
            AddColumn("dbo.Properties", "RepairType", c => c.Int(nullable: false));
            AddColumn("dbo.Properties", "PropertyMaterial", c => c.Int(nullable: false));
            AddColumn("dbo.Properties", "YearOfConstruction", c => c.Int());
            AddColumn("dbo.Properties", "FloorsNumber", c => c.Int());
            AddColumn("dbo.Properties", "FloorsOutOf", c => c.Int());
            AddColumn("dbo.Properties", "Tax", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Properties", "TaxPercent", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Properties", "TaxInclude", c => c.Boolean());
            AddColumn("dbo.Properties", "SecurityPayment", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Properties", "ClientCommission", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Properties", "LandArea", c => c.Decimal(precision: 9, scale: 2));
            AddColumn("dbo.Properties", "ParkingType", c => c.Int());
            AddColumn("dbo.Properties", "ParkingSize", c => c.Int());
            AddColumn("dbo.Properties", "ParkingCost", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropertyFloors", "PropertyEntity_Uid", "dbo.Properties");
            DropForeignKey("dbo.PropertyFloors", "FloorTypeUid", "dbo.PropertyFloorTypes");
            DropForeignKey("dbo.PropertyAdditionalElements", "PropertyUid", "dbo.Properties");
            DropForeignKey("dbo.PropertyAdditionalElements", "AdditionalElementUid", "dbo.AdditionalElements");
            DropIndex("dbo.PropertyFloors", new[] { "PropertyEntity_Uid" });
            DropIndex("dbo.PropertyFloors", new[] { "FloorTypeUid" });
            DropIndex("dbo.PropertyAdditionalElements", new[] { "AdditionalElementUid" });
            DropIndex("dbo.PropertyAdditionalElements", new[] { "PropertyUid" });
            DropColumn("dbo.Properties", "ParkingCost");
            DropColumn("dbo.Properties", "ParkingSize");
            DropColumn("dbo.Properties", "ParkingType");
            DropColumn("dbo.Properties", "LandArea");
            DropColumn("dbo.Properties", "ClientCommission");
            DropColumn("dbo.Properties", "SecurityPayment");
            DropColumn("dbo.Properties", "TaxInclude");
            DropColumn("dbo.Properties", "TaxPercent");
            DropColumn("dbo.Properties", "Tax");
            DropColumn("dbo.Properties", "FloorsOutOf");
            DropColumn("dbo.Properties", "FloorsNumber");
            DropColumn("dbo.Properties", "YearOfConstruction");
            DropColumn("dbo.Properties", "PropertyMaterial");
            DropColumn("dbo.Properties", "RepairType");
            DropColumn("dbo.Properties", "PropertyType");
            DropColumn("dbo.Properties", "Condition");
            DropTable("dbo.PropertyFloorTypes");
            DropTable("dbo.PropertyFloors");
            DropTable("dbo.AdditionalElements");
            DropTable("dbo.PropertyAdditionalElements");
        }
    }
}
