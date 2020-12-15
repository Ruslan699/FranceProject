namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 500),
                        Type = c.Int(nullable: false),
                        Extension = c.String(nullable: false, maxLength: 25),
                        Body = c.Binary(nullable: false),
                        PropertyUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.Properties", t => t.PropertyUid, cascadeDelete: true)
                .Index(t => t.PropertyUid);
            
            AddColumn("dbo.Properties", "TotalSquare", c => c.Decimal(nullable: false, precision: 9, scale: 2));
            DropColumn("dbo.Properties", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Properties", "Image", c => c.Binary());
            DropForeignKey("dbo.Documents", "PropertyUid", "dbo.Properties");
            DropIndex("dbo.Documents", new[] { "PropertyUid" });
            DropColumn("dbo.Properties", "TotalSquare");
            DropTable("dbo.Documents");
        }
    }
}
