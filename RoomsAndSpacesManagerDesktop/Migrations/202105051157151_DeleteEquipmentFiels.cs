namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteEquipmentFiels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RaSM_Rooms", "Equipment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaSM_Rooms", "Equipment", c => c.String());
        }
    }
}
