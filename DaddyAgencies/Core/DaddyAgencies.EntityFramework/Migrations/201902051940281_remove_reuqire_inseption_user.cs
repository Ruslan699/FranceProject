namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_reuqire_inseption_user : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Inseptions", "RequesterUserUid", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Inseptions", "RequesterUserUid", c => c.Guid(nullable: false));
        }
    }
}
