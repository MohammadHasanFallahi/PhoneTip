namespace PhoneTipProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phonetipmigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "Tags");
        }
    }
}
