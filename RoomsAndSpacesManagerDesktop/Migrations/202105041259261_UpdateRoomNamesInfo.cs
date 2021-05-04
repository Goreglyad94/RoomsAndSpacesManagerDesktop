namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoomNamesInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_RoomNames", "Min_area", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Class_chistoti_SanPin", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Class_chistoti_SP_158", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Class_chistoti_GMP", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "T_calc", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "T_min", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "T_max", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Pritok", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Vityazhka", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Ot_vlazhnost", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "KEO_est_osv", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "KEO_sovm_osv", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Discription_OV", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Osveshennost_pro_obshem_osvech", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Group_el_bez", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Discription_EOM", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Discription_AR", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Equipment_VK", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Discription_SS", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Discription_AK_ATH", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Discription_GSV", c => c.String());
            AddColumn("dbo.RaSM_RoomNames", "Discription_HS", c => c.String());
            DropColumn("dbo.RaSM_Rooms", "Area");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaSM_Rooms", "Area", c => c.Double(nullable: false));
            DropColumn("dbo.RaSM_RoomNames", "Discription_HS");
            DropColumn("dbo.RaSM_RoomNames", "Discription_GSV");
            DropColumn("dbo.RaSM_RoomNames", "Discription_AK_ATH");
            DropColumn("dbo.RaSM_RoomNames", "Discription_SS");
            DropColumn("dbo.RaSM_RoomNames", "Equipment_VK");
            DropColumn("dbo.RaSM_RoomNames", "Discription_AR");
            DropColumn("dbo.RaSM_RoomNames", "Discription_EOM");
            DropColumn("dbo.RaSM_RoomNames", "Group_el_bez");
            DropColumn("dbo.RaSM_RoomNames", "Osveshennost_pro_obshem_osvech");
            DropColumn("dbo.RaSM_RoomNames", "Discription_OV");
            DropColumn("dbo.RaSM_RoomNames", "KEO_sovm_osv");
            DropColumn("dbo.RaSM_RoomNames", "KEO_est_osv");
            DropColumn("dbo.RaSM_RoomNames", "Ot_vlazhnost");
            DropColumn("dbo.RaSM_RoomNames", "Vityazhka");
            DropColumn("dbo.RaSM_RoomNames", "Pritok");
            DropColumn("dbo.RaSM_RoomNames", "T_max");
            DropColumn("dbo.RaSM_RoomNames", "T_min");
            DropColumn("dbo.RaSM_RoomNames", "T_calc");
            DropColumn("dbo.RaSM_RoomNames", "Class_chistoti_GMP");
            DropColumn("dbo.RaSM_RoomNames", "Class_chistoti_SP_158");
            DropColumn("dbo.RaSM_RoomNames", "Class_chistoti_SanPin");
            DropColumn("dbo.RaSM_RoomNames", "Min_area");
        }
    }
}
