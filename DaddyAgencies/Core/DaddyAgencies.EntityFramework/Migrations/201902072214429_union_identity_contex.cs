namespace DaddyAgencies.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class union_identity_contex : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.WebUserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.WebRoles", t => t.RoleId)
                .ForeignKey("dbo.WebUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.WebUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(maxLength: 256),
                        PhoneNumber = c.String(),
                        FullName = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.WebUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WebUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.WebUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebUserRoles", "UserId", "dbo.WebUsers");
            DropForeignKey("dbo.WebUserLogins", "UserId", "dbo.WebUsers");
            DropForeignKey("dbo.WebUserClaims", "UserId", "dbo.WebUsers");
            DropForeignKey("dbo.WebUserRoles", "RoleId", "dbo.WebRoles");
            DropIndex("dbo.WebUserLogins", new[] { "UserId" });
            DropIndex("dbo.WebUserClaims", new[] { "UserId" });
            DropIndex("dbo.WebUsers", "UserNameIndex");
            DropIndex("dbo.WebUserRoles", new[] { "RoleId" });
            DropIndex("dbo.WebUserRoles", new[] { "UserId" });
            DropIndex("dbo.WebRoles", "RoleNameIndex");
            DropTable("dbo.WebUserLogins");
            DropTable("dbo.WebUserClaims");
            DropTable("dbo.WebUsers");
            DropTable("dbo.WebUserRoles");
            DropTable("dbo.WebRoles");
        }
    }
}
