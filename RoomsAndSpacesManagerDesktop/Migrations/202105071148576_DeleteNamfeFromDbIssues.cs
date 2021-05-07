namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteNamfeFromDbIssues : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RaSM_Rooms", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaSM_Rooms", "Name", c => c.String());
        }
    }
}
