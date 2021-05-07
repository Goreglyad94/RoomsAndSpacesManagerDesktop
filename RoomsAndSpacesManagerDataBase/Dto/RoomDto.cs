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

        public int RoomNameId { get; set; }

        public string ShortName { get; set; }
        public string RoomNumber { get; set; }
        public int ArRoomId { get; set; }
        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }
        #endregion
    }
}
