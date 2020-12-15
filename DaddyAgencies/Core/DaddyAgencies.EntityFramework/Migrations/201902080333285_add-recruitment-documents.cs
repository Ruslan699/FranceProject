namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrecruitmentdocuments : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Documents", newName: "PropertyDocuments");
            CreateTable(
                "dbo.RecruitmentDocuments",
                c => new
                    {
                        Uid = c.Guid(nullable: false),
                        RecruitmentUid = c.Guid(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 500),
                        Type = c.Int(nullable: false),
                        Extension = c.String(nullable: false, maxLength: 25),
                        Body = c.Binary(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.Recruitments", t => t.RecruitmentUid)
                .Index(t => t.RecruitmentUid);
            
            AlterColumn("dbo.Recruitments", "FullName", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Recruitments", "PhoneNumber", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Recruitments", "Email", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Recruitments", "Message", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecruitmentDocuments", "RecruitmentUid", "dbo.Recruitments");
            DropIndex("dbo.RecruitmentDocuments", new[] { "RecruitmentUid" });
            AlterColumn("dbo.Recruitments", "Message", c => c.String());
            AlterColumn("dbo.Recruitments", "Email", c => c.String());
            AlterColumn("dbo.Recruitments", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Recruitments", "FullName", c => c.String());
            DropTable("dbo.RecruitmentDocuments");
            RenameTable(name: "dbo.PropertyDocuments", newName: "Documents");
        }
    }
}
