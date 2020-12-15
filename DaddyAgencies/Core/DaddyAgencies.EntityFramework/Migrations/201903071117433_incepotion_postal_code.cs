namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incepotion_postal_code : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inseptions", "PostalCodeUid", c => c.Guid());
            CreateIndex("dbo.Inseptions", "PostalCodeUid");
            AddForeignKey("dbo.Inseptions", "PostalCodeUid", "dbo.PostalCodes", "Uid");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inseptions", "PostalCodeUid", "dbo.PostalCodes");
            DropIndex("dbo.Inseptions", new[] { "PostalCodeUid" });
            DropColumn("dbo.Inseptions", "PostalCodeUid");
        }
    }
}
