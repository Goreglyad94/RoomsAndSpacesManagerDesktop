namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInIssueRoomNamesProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "Min_area", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Class_chistoti_SanPin", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Class_chistoti_SP_158", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Class_chistoti_GMP", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "T_calc", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "T_min", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "T_max", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Pritok", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Vityazhka", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Ot_vlazhnost", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "KEO_est_osv", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "KEO_sovm_osv", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Discription_OV", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Osveshennost_pro_obshem_osvech", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Group_el_bez", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Discription_EOM", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Discription_AR", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Equipment_VK", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Discription_SS", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Discription_AK_ATH", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Discription_GSV", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Categoty_Chistoti_po_san_epid", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Discription_HS", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "Discription_HS");
            DropColumn("dbo.RaSM_Rooms", "Categoty_Chistoti_po_san_epid");
            DropColumn("dbo.RaSM_Rooms", "Discription_GSV");
            DropColumn("dbo.RaSM_Rooms", "Discription_AK_ATH");
            DropColumn("dbo.RaSM_Rooms", "Discription_SS");
            DropColumn("dbo.RaSM_Rooms", "Equipment_VK");
            DropColumn("dbo.RaSM_Rooms", "Discription_AR");
            DropColumn("dbo.RaSM_Rooms", "Discription_EOM");
            DropColumn("dbo.RaSM_Rooms", "Group_el_bez");
            DropColumn("dbo.RaSM_Rooms", "Osveshennost_pro_obshem_osvech");
            DropColumn("dbo.RaSM_Rooms", "Discription_OV");
            DropColumn("dbo.RaSM_Rooms", "KEO_sovm_osv");
            DropColumn("dbo.RaSM_Rooms", "KEO_est_osv");
            DropColumn("dbo.RaSM_Rooms", "Ot_vlazhnost");
            DropColumn("dbo.RaSM_Rooms", "Vityazhka");
            DropColumn("dbo.RaSM_Rooms", "Pritok");
            DropColumn("dbo.RaSM_Rooms", "T_max");
            DropColumn("dbo.RaSM_Rooms", "T_min");
            DropColumn("dbo.RaSM_Rooms", "T_calc");
            DropColumn("dbo.RaSM_Rooms", "Class_chistoti_GMP");
            DropColumn("dbo.RaSM_Rooms", "Class_chistoti_SP_158");
            DropColumn("dbo.RaSM_Rooms", "Class_chistoti_SanPin");
            DropColumn("dbo.RaSM_Rooms", "Min_area");
        }
    }
}
