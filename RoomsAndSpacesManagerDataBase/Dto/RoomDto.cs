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

        public RoomDto()
        {

        }

        #region Поля для выгрузки
        public int Id { get; set; }

        private string name;


        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }


        private int roomNameId;
        public int RoomNameId
        {
            get => roomNameId;
            set
            {
                roomNameId = value;
                if (RoomNameId != 0)
                    Name = context.RaSM_RoomNames.FirstOrDefault(x => x.Id == RoomNameId).Name;
            }
        }

        public string ShortName { get; set; }
        public string RoomNumber { get; set; }

        #region Исходные данные по помещениям
        public string Min_area { get; set; }
        public string Class_chistoti_SanPin { get; set; }
        public string Class_chistoti_SP_158 { get; set; }
        public string Class_chistoti_GMP { get; set; }
        public string T_calc { get; set; }
        public string T_min { get; set; }
        public string T_max { get; set; }
        public string Pritok { get; set; }
        public string Vityazhka { get; set; }
        public string Ot_vlazhnost { get; set; }
        public string KEO_est_osv { get; set; }
        public string KEO_sovm_osv { get; set; }
        public string Discription_OV { get; set; }
        public string Osveshennost_pro_obshem_osvech { get; set; }
        public string Group_el_bez { get; set; }
        public string Discription_EOM { get; set; }
        public string Discription_AR { get; set; }
        public string Equipment_VK { get; set; }
        public string Discription_SS { get; set; }
        public string Discription_AK_ATH { get; set; }
        public string Discription_GSV { get; set; }
        public string Categoty_Chistoti_po_san_epid { get; set; }
        public string Discription_HS { get; set; }
        #endregion

        public int ArRoomId { get; set; }
        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }
        #endregion
    }
}
