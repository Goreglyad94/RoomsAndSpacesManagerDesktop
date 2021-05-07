namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoomNameId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "RoomNameId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "RoomNameId");
        }
    }
}
