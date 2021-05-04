namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoty_Chistoti_po_san_epidFieldToRoomNameDto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_RoomNames", "Categoty_Chistoti_po_san_epid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_RoomNames", "Categoty_Chistoti_po_san_epid");
        }
    }
}
