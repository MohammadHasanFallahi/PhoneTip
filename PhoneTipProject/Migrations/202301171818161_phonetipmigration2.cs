namespace PhoneTipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phonetipmigration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pages", "Tags", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pages", "Tags", c => c.String());
        }
    }
}
