using System.ComponentModel.DataAnnotations.Schema;

namespace RoomsAndSpacesManagerDesktop.DTO
{
    [Table("RaSM_Rooms")]
    public class RoomDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public string RoomNumber { get; set; }
        public double Area { get; set; }

        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }
    }
}
