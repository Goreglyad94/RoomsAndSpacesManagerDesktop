namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteTest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RaSM_Projects", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaSM_Projects", "Test", c => c.String());
        }
    }
}
