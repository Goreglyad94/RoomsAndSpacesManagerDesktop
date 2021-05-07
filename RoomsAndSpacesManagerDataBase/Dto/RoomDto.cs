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
        private string calc;
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
        private string max;
        private string min;


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
        public string Class_chistoti_SanPin { get => class_chistoti_SanPin; set => Set(ref class_chistoti_SanPin, value); }
        public string Class_chistoti_SP_158 { get => class_chistoti_SP_158; set => Set(ref class_chistoti_SP_158, value); }
        public string Class_chistoti_GMP { get => class_chistoti_GMP; set => Set(ref class_chistoti_GMP, value); }
        public string T_calc { get => calc; set => Set(ref calc, value); }
        public string T_min { get => min; set => Set(ref min, value); }
        public string T_max { get => max; set => Set(ref max, value); }
        public string Pritok { get => pritok; set => Set(ref pritok, value); }
        public string Vityazhka { get => vityazhka; set => Set(ref vityazhka, value); }
        public string Ot_vlazhnost { get => ot_vlazhnost; set => Set(ref ot_vlazhnost, value); }
        public string KEO_est_osv { get => kEO_est_osv; set => Set(ref kEO_est_osv, value); }
        public string KEO_sovm_osv { get => kEO_sovm_osv; set => Set(ref kEO_sovm_osv, value); }
        public string Discription_OV { get => discription_OV; set => Set(ref discription_OV, value); }
        public string Osveshennost_pro_obshem_osvech { get => osveshennost_pro_obshem_osvech; set => Set(ref osveshennost_pro_obshem_osvech, value); }
        public string Group_el_bez { get => group_el_bez; set => Set(ref group_el_bez, value); }
        public string Discription_EOM { get => discription_EOM; set => Set(ref discription_EOM, value); }
        public string Discription_AR { get => discription_AR; set => Set(ref discription_AR, value); }
        public string Equipment_VK { get => equipment_VK; set => Set(ref equipment_VK, value); }
        public string Discription_SS { get => discription_SS; set => Set(ref discription_SS, value); }
        public string Discription_AK_ATH { get => discription_AK_ATH; set => Set(ref discription_AK_ATH, value); }
        public string Discription_GSV { get => discription_GSV; set => Set(ref discription_GSV, value); }
        public string Categoty_Chistoti_po_san_epid { get => categoty_Chistoti_po_san_epid; set => Set(ref categoty_Chistoti_po_san_epid, value); }
        public string Discription_HS { get => discription_HS; set => Set(ref discription_HS, value); }
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

        #endregion




        #endregion

        public virtual BuildingDto Building { get; set; }
        #endregion
    }
}
