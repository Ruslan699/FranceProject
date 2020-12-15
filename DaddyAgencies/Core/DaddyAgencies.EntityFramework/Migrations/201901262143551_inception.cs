namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inception : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inceptions",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        CustomerFullName = c.String(nullable: false, maxLength: 250),
                        CustomerEMail = c.String(nullable: false, maxLength: 250),
                        CustomerPhone = c.String(nullable: false, maxLength: 250),
                        RequestedDateUtc = c.DateTime(nullable: false),
                        ConfirmedDateUtc = c.DateTime(),
                        Status = c.Int(nullable: false),
                        PropertyUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.Properties", t => t.PropertyUid, cascadeDelete: true)
                .Index(t => t.PropertyUid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inceptions", "PropertyUid", "dbo.Properties");
            DropIndex("dbo.Inceptions", new[] { "PropertyUid" });
            DropTable("dbo.Inceptions");
        }
    }
}
