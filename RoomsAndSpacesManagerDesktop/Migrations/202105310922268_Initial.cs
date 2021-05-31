namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Projects", "Test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Projects", "Test");
        }
    }
}
