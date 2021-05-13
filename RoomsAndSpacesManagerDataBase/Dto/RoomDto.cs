using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDataBase.Dto
{
    [Table("RaSM_Rooms")]
    public class RoomDto : ViewModel
    {
        static RoomAndSpacesDbContext context = new RoomAndSpacesDbContext();
        private int roomNameId;
        private string min_area;
        private string class_chistoti_SanPin;
        private string class_chistoti_SP_158;
        private string class_chistoti_GMP;
        private string t_calc;
        private string discription_HS;
        private string categoty_Chistoti_po_san_epid;
        private string discription_GSV;
        private string discription_AK_ATH;
        private string discription_SS;
        private string equipment_VK;
        private string discription_AR;
        private string discription_EOM;
        private string group_el_bez;
        private string osveshennost_pro_obshem_osvech;
        private string discription_OV;
        private string kEO_sovm_osv;
        private string kEO_est_osv;
        private string ot_vlazhnost;
        private string vityazhka;
        private string pritok;
        private string t_max;
        private string t_min;


        #region Поля для выгрузки
        public int Id { get; set; }

        private string name;

        [NotMapped]
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public int RoomNameId
        {
            get => roomNameId;
            set
            {
                roomNameId = value;
                if (RoomNameId != 0)
                {
                    RoomName = context.RaSM_RoomNames.FirstOrDefault(x => x.Id == RoomNameId);
                    Name = RoomName.Name;
                }

            }
        }

        public string ShortName { get; set; }
        public string RoomNumber { get; set; }

        #region Исходные данные по помещениям
        public string Min_area
        {
            get => min_area;
            set 
            {
                Set(ref min_area, value); 
                if (RoomName != null)
                {
                    if (Min_area != RoomName.Min_area)
                    {
                        Min_area_color = "LightCoral";
                    }
                    else
                    {
                        Min_area_color = "Transparent";
                    }
                }
                
            }
        }

        public int? Count { get; set; }
        public int? Summary_Area { get; set; }

        public string Class_chistoti_SanPin 
        { 
            get => class_chistoti_SanPin;
            set 
            {
                Set(ref class_chistoti_SanPin, value);
                if (RoomName != null)
                {
                    if (Class_chistoti_SanPin != RoomName.Class_chistoti_SanPin && Class_chistoti_SanPin != null)
                    {
                        Class_chistoti_SanPin_color = "LightCoral";
                    }
                    else
                    {
                        Class_chistoti_SanPin_color = "Transparent";
                    }
                }
            }
        }
        public string Class_chistoti_SP_158 
        {
            get => class_chistoti_SP_158;
            set 
            {
                Set(ref class_chistoti_SP_158, value);
                if (RoomName != null)
                {
                    if (Class_chistoti_SP_158 != RoomName.Class_chistoti_SP_158 && Class_chistoti_SP_158 != null)
                    {
                        Class_chistoti_SP_158_color = "LightCoral";
                    }
                    else
                    {
                        Class_chistoti_SP_158_color = "Transparent";
                    }
                }
            }
        }
        public string Class_chistoti_GMP 
        {
            get => class_chistoti_GMP;
            set 
            {
                Set(ref class_chistoti_GMP, value);
                if (RoomName != null)
                {
                    if (Class_chistoti_GMP != RoomName.Class_chistoti_GMP && Class_chistoti_GMP != null)
                    {
                        Class_chistoti_GMP_color = "LightCoral";
                    }
                    else
                    {
                        Class_chistoti_GMP_color = "Transparent";
                    }
                }
            }
        }
        public string T_calc 
        {
            get => t_calc;
            set 
            {
                Set(ref t_calc, value);
                if (RoomName != null)
                {
                    if (T_calc != RoomName.T_calc && T_calc != null)
                    {
                        T_calc_color = "LightCoral";
                    }
                    else
                    {
                        T_calc_color = "Transparent";
                    }
                }
            }
        }
        public string T_min 
        {
            get => t_min;
            set 
            {
                Set(ref t_min, value);
                if (RoomName != null)
                {
                    if (T_min != RoomName.T_min && T_min != null)
                    {
                        T_min_color = "LightCoral";
                    }
                    else
                    {
                        T_min_color = "Transparent";
                    }
                }
            }
        }
        public string T_max 
        {
            get => t_max;
            set 
            {
                Set(ref t_max, value);
                if (RoomName != null)
                {
                    if (T_max != RoomName.T_max && T_max != null)
                    {
                        T_max_color = "LightCoral";
                    }
                    else
                    {
                        T_max_color = "Transparent";
                    }
                }
            }
        }
        public string Pritok 
        {
            get => pritok;
            set 
            {
                Set(ref pritok, value);
                if (RoomName != null)
                {
                    if (Pritok != RoomName.Pritok && Pritok != null)
                    {
                        Pritok_color = "LightCoral";
                    }
                    else
                    {
                        Pritok_color = "Transparent";
                    }
                }
            }
        }
        public string Vityazhka 
        {
            get => vityazhka;
            set 
            {
                Set(ref vityazhka, value);
                if (RoomName != null)
                {
                    if (Vityazhka != RoomName.Vityazhka && Vityazhka != null)
                    {
                        Vityazhka_color = "LightCoral";
                    }
                    else
                    {
                        Vityazhka_color = "Transparent";
                    }
                }
            }
        }
        public string Ot_vlazhnost 
        {
            get => ot_vlazhnost;
            set 
            {
                Set(ref ot_vlazhnost, value);
                if (RoomName != null)
                {
                    if (Ot_vlazhnost != RoomName.Ot_vlazhnost && Ot_vlazhnost != null)
                    {
                        Ot_vlazhnost_color = "LightCoral";
                    }
                    else
                    {
                        Ot_vlazhnost_color = "Transparent";
                    }
                }
            }
        }
        public string KEO_est_osv 
        {
            get => kEO_est_osv;
            set 
            {
                Set(ref kEO_est_osv, value);
                if (RoomName != null)
                {
                    if (KEO_est_osv != RoomName.KEO_est_osv && KEO_est_osv != null)
                    {
                        KEO_est_osv_color = "LightCoral";
                    }
                    else
                    {
                        KEO_est_osv_color = "Transparent";
                    }
                }
            }
        }
        public string KEO_sovm_osv 
        {
            get => kEO_sovm_osv;
            set 
            {
                Set(ref kEO_sovm_osv, value);
                if (RoomName != null)
                {
                    if (KEO_sovm_osv != RoomName.KEO_sovm_osv && KEO_sovm_osv != null)
                    {
                        KEO_sovm_osv_color = "LightCoral";
                    }
                    else
                    {
                        KEO_sovm_osv_color = "Transparent";
                    }
                }
            }
        }
        public string Discription_OV 
        {
            get => discription_OV;
            set 
            {
                Set(ref discription_OV, value);
                if (RoomName != null)
                {
                    if (Discription_OV != RoomName.Discription_OV && Discription_OV != null)
                    {
                        Discription_OV_color = "LightCoral";
                    }
                    else
                    {
                        Discription_OV_color = "Transparent";
                    }
                }
            }
        }
        public string Osveshennost_pro_obshem_osvech 
        {
            get => osveshennost_pro_obshem_osvech;
            set 
            {
                Set(ref osveshennost_pro_obshem_osvech, value);
                if (RoomName != null)
                {
                    if (Osveshennost_pro_obshem_osvech != RoomName.Osveshennost_pro_obshem_osvech && Osveshennost_pro_obshem_osvech != null)
                    {
                        Osveshennost_pro_obshem_osvech_color = "LightCoral";
                    }
                    else
                    {
                        Osveshennost_pro_obshem_osvech_color = "Transparent";
                    }
                }
            }
        }
        public string Group_el_bez 
        {
            get => group_el_bez;
            set 
            {
                Set(ref group_el_bez, value);
                if (RoomName != null)
                {
                    if (Group_el_bez != RoomName.Group_el_bez && Group_el_bez != null)
                    {
                        Group_el_bez_color = "LightCoral";
                    }
                    else
                    {
                        Group_el_bez_color = "Transparent";
                    }
                }
            }
        }
        public string Discription_EOM 
        {
            get => discription_EOM;
            set 
            {
                Set(ref discription_EOM, value);
                if (RoomName != null)
                {
                    if (Discription_EOM != RoomName.Discription_EOM && Discription_EOM != null)
                    {
                        Discription_EOM_color = "LightCoral";
                    }
                    else
                    {
                        Discription_EOM_color = "Transparent";
                    }
                }
            }
        }
        public string Discription_AR 
        {
            get => discription_AR;
            set 
            {
                Set(ref discription_AR, value);
                if (RoomName != null)
                {
                    if (Discription_AR != RoomName.Discription_AR && Discription_AR != null)
                    {
                        Discription_AR_color = "LightCoral";
                    }
                    else
                    {
                        Discription_AR_color = "Transparent";
                    }
                }
            }
        }
        public string Equipment_VK 
        {
            get => equipment_VK;
            set 
            {
                Set(ref equipment_VK, value);
                if (RoomName != null)
                {
                    if (Equipment_VK != RoomName.Equipment_VK && Equipment_VK != null)
                    {
                        Equipment_VK_color = "LightCoral";
                    }
                    else
                    {
                        Equipment_VK_color = "Transparent";
                    }
                }
            }
        }
        public string Discription_SS 
        {
            get => discription_SS;
            set 
            {
                Set(ref discription_SS, value);
                if (RoomName != null)
                {
                    if (Discription_SS != RoomName.Discription_SS && Discription_SS != null)
                    {
                        Discription_SS_color = "LightCoral";
                    }
                    else
                    {
                        Discription_SS_color = "Transparent";
                    }
                }
            }
        }
        public string Discription_AK_ATH 
        {
            get => discription_AK_ATH;
            set 
            {
                Set(ref discription_AK_ATH, value);
                if (RoomName != null)
                {
                    if (Discription_AK_ATH != RoomName.Discription_AK_ATH && Discription_AK_ATH != null)
                    {
                        Discription_AK_ATH_color = "LightCoral";
                    }
                    else
                    {
                        Discription_AK_ATH_color = "Transparent";
                    }
                }
            }
        }
        public string Discription_GSV 
        {
            get => discription_GSV;
            set 
            {
                Set(ref discription_GSV, value);
                if (RoomName != null)
                {
                    if (Discription_GSV != RoomName.Discription_GSV && Discription_GSV != null)
                    {
                        Discription_GSV_color = "LightCoral";
                    }
                    else
                    {
                        Discription_GSV_color = "Transparent";
                    }
                }
            }
        }
        public string Categoty_Chistoti_po_san_epid 
        {
            get => categoty_Chistoti_po_san_epid;
            set 
            {
                Set(ref categoty_Chistoti_po_san_epid, value);
                if (RoomName != null)
                {
                    if (Categoty_Chistoti_po_san_epid != RoomName.Categoty_Chistoti_po_san_epid && Categoty_Chistoti_po_san_epid != null)
                    {
                        Categoty_Chistoti_po_san_epid_color = "LightCoral";
                    }
                    else
                    {
                        Categoty_Chistoti_po_san_epid_color = "Transparent";
                    }
                }
            }
        }
        public string Discription_HS 
        {
            get => discription_HS;
            set 
            {
                Set(ref discription_HS, value);
                if (RoomName != null)
                {
                    if (Discription_HS != RoomName.Discription_HS && Discription_HS != null)
                    {
                        Discription_HS_color = "LightCoral";
                    }
                    else
                    {
                        Discription_HS_color = "Transparent";
                    }
                }
            }
        }

        public string Categoty_pizharoopasnosti { get; set; }
        public string Rab_mesta_posetiteli { get; set; }
        public string Nagruzki_na_perekririe { get; set; }
        public string El_Nagruzka { get; set; }
        #endregion

        public int ArRoomId { get; set; }
        public int BuildingId { get; set; }



        #region SupClass
        [NotMapped]
        public RoomNameDto RoomName { get; set; }




        #region Свойства для цветов
        private string min_area_color;
        [NotMapped]
        public string Min_area_color
        {
            get { return min_area_color; }
            set { Set(ref min_area_color, value); }
        }

        private string class_chistoti_SanPin_color;
        [NotMapped]
        public string Class_chistoti_SanPin_color
        {
            get { return class_chistoti_SanPin_color; }
            set { Set(ref class_chistoti_SanPin_color, value); }
        }


        private string class_chistoti_SP_158_color;
        [NotMapped]
        public string Class_chistoti_SP_158_color
        {
            get { return class_chistoti_SP_158_color; }
            set { Set(ref class_chistoti_SP_158_color, value); }
        }

        private string class_chistoti_GMP_color;
        [NotMapped]
        public string Class_chistoti_GMP_color
        {
            get { return class_chistoti_GMP_color; }
            set { Set(ref class_chistoti_GMP_color, value); }
        }

        private string t_calc_color;
        [NotMapped]
        public string T_calc_color
        {
            get { return t_calc_color; }
            set { Set(ref t_calc_color, value); }
        }

        private string t_min_color;
        [NotMapped]
        public string T_min_color
        {
            get { return t_min_color; }
            set { Set(ref t_min_color, value); }
        }

        private string t_max_color;
        [NotMapped]
        public string T_max_color
        {
            get { return t_max_color; }
            set { Set(ref t_max_color, value); }
        }

        private string pritok_color;
        [NotMapped]
        public string Pritok_color
        {
            get { return pritok_color; }
            set { Set(ref pritok_color, value); }
        }

        private string vityazhka_color;
        [NotMapped]
        public string Vityazhka_color
        {
            get { return vityazhka_color; }
            set { Set(ref vityazhka_color, value); }
        }

        private string ot_vlazhnost_color;
        [NotMapped]
        public string Ot_vlazhnost_color
        {
            get { return ot_vlazhnost_color; }
            set { Set(ref ot_vlazhnost_color, value); }
        }

        private string kEO_est_osv_color;
        [NotMapped]
        public string KEO_est_osv_color
        {
            get { return kEO_est_osv_color; }
            set { Set(ref kEO_est_osv_color, value); }
        }

        private string kEO_sovm_osv_color;
        [NotMapped]
        public string KEO_sovm_osv_color
        {
            get { return kEO_sovm_osv_color; }
            set { Set(ref kEO_sovm_osv_color, value); }
        }

        private string discription_OV_color;
        [NotMapped]
        public string Discription_OV_color
        {
            get { return discription_OV_color; }
            set { Set(ref discription_OV_color, value); }
        }

        private string osveshennost_pro_obshem_osvech_color;
        [NotMapped]
        public string Osveshennost_pro_obshem_osvech_color
        {
            get { return osveshennost_pro_obshem_osvech_color; }
            set { Set(ref osveshennost_pro_obshem_osvech_color, value); }
        }

        private string group_el_bez_color;
        [NotMapped]
        public string Group_el_bez_color
        {
            get { return group_el_bez_color; }
            set { Set(ref group_el_bez_color, value); }
        }

        private string discription_EOM_color;
        [NotMapped]
        public string Discription_EOM_color
        {
            get { return discription_EOM_color; }
            set { Set(ref discription_EOM_color, value); }
        }

        private string discription_AR_color;
        [NotMapped]
        public string Discription_AR_color
        {
            get { return discription_AR_color; }
            set { Set(ref discription_AR_color, value); }
        }

        private string equipment_VK_color;
        [NotMapped]
        public string Equipment_VK_color
        {
            get { return equipment_VK_color; }
            set { Set(ref equipment_VK_color, value); }
        }

        private string discription_SS_color;
        [NotMapped]
        public string Discription_SS_color
        {
            get { return discription_SS_color; }
            set { Set(ref discription_SS_color, value); }
        }

        private string discription_AK_ATH_color;
        [NotMapped]
        public string Discription_AK_ATH_color
        {
            get { return discription_AK_ATH_color; }
            set { Set(ref discription_AK_ATH_color, value); }
        }

        private string discription_GSV_color;
        [NotMapped]
        public string Discription_GSV_color
        {
            get { return discription_GSV_color; }
            set { Set(ref discription_GSV_color, value); }
        }

        private string categoty_Chistoti_po_san_epid_color;
        [NotMapped]
        public string Categoty_Chistoti_po_san_epid_color
        {
            get { return categoty_Chistoti_po_san_epid_color; }
            set { Set(ref categoty_Chistoti_po_san_epid_color, value); }
        }

        private string discription_HS_color;
        [NotMapped]
        public string Discription_HS_color
        {
            get { return discription_HS_color; }
            set { Set(ref discription_HS_color, value); }
        }


        #endregion




        #endregion

        public virtual BuildingDto Building { get; set; }
        #endregion
    }
}
