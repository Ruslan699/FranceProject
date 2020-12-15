namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inseptionedited : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Inceptions", newName: "Inseptions");
            AddColumn("dbo.Inseptions", "ConfirmedAddress", c => c.String(maxLength: 500));
            AlterColumn("dbo.Inseptions", "CustomerEmail", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Inseptions", "CustomerPhone", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Inseptions", "CustomerPhone", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Inseptions", "CustomerEmail", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Inseptions", "ConfirmedAddress");
            RenameTable(name: "dbo.Inseptions", newName: "Inceptions");
        }
    }
}
