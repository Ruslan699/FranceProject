namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPostalCodes",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        UserUid = c.Guid(nullable: false),
                        PostalCodeUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPostalCodes");
        }
    }
}
