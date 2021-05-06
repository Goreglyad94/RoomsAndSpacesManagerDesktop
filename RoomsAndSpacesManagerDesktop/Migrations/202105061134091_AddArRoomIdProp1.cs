namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArRoomIdProp1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "ArRoomId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "ArRoomId");
        }
    }
}
