namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recruitment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recruitments",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        FullName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Message = c.String(),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recruitments");
        }
    }
}
