namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreign_keys_for_users : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhysicalPersons", "UserUid", c => c.Guid());
            CreateIndex("dbo.Inseptions", "RequesterUserUid");
            CreateIndex("dbo.Inseptions", "ConfirmerUserUid");
            CreateIndex("dbo.PhysicalPersons", "UserUid");
            AddForeignKey("dbo.Inseptions", "ConfirmerUserUid", "dbo.WebUsers", "Id");
            AddForeignKey("dbo.Inseptions", "RequesterUserUid", "dbo.WebUsers", "Id");
            AddForeignKey("dbo.PhysicalPersons", "UserUid", "dbo.WebUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhysicalPersons", "UserUid", "dbo.WebUsers");
            DropForeignKey("dbo.Inseptions", "RequesterUserUid", "dbo.WebUsers");
            DropForeignKey("dbo.Inseptions", "ConfirmerUserUid", "dbo.WebUsers");
            DropIndex("dbo.PhysicalPersons", new[] { "UserUid" });
            DropIndex("dbo.Inseptions", new[] { "ConfirmerUserUid" });
            DropIndex("dbo.Inseptions", new[] { "RequesterUserUid" });
            AlterColumn("dbo.PhysicalPersons", "UserUid", c => c.Guid(nullable: false));
        }
    }
}
