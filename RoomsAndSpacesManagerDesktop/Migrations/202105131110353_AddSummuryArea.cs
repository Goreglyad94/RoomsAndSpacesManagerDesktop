namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSummuryArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "Count", c => c.Int());
            AddColumn("dbo.RaSM_Rooms", "Summary_Area", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "Summary_Area");
            DropColumn("dbo.RaSM_Rooms", "Count");
        }
    }
}
