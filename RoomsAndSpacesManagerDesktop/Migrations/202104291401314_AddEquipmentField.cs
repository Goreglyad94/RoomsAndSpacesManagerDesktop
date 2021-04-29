namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipmentField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "Equipment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "Equipment");
        }
    }
}
