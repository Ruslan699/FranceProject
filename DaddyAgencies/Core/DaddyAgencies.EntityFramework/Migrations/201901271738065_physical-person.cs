namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class physicalperson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhysicalPersons",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        Surname = c.String(nullable: false, maxLength: 250),
                        Name = c.String(nullable: false, maxLength: 250),
                        LastName = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 128),
                        PhoneNumber = c.String(nullable: false, maxLength: 128),
                        Skype = c.String(maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        Gender = c.Boolean(),
                        Address = c.String(maxLength: 500),
                        UserUid = c.Guid(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid);
            
            AddColumn("dbo.Inceptions", "RequesterUserUid", c => c.Guid(nullable: false));
            AddColumn("dbo.Inceptions", "ConfirmerUserUid", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inceptions", "ConfirmerUserUid");
            DropColumn("dbo.Inceptions", "RequesterUserUid");
            DropTable("dbo.PhysicalPersons");
        }
    }
}
