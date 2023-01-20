namespace PhoneTipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phonetipmigration3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleTitel = c.String(nullable: false, maxLength: 250),
                        RoleName = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        PassWord = c.String(nullable: false, maxLength: 200),
                        ActiveCode = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
