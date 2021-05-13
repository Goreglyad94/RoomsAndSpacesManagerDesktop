namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "Categoty_pizharoopasnosti", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Rab_mesta_posetiteli", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Nagruzki_na_perekririe", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "El_Nagruzka", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "El_Nagruzka");
            DropColumn("dbo.RaSM_Rooms", "Nagruzki_na_perekririe");
            DropColumn("dbo.RaSM_Rooms", "Rab_mesta_posetiteli");
            DropColumn("dbo.RaSM_Rooms", "Categoty_pizharoopasnosti");
        }
    }
}
