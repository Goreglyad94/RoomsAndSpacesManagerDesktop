using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomsAndSpacesManagerDesktop.DTO
{
    [Table("RaSM_Buildings")]
    public class BuildingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int ProjectId { get; set; }

        public virtual ProjectDto Project { get; set; }

        public virtual ICollection<RoomDto> Rooms { get; set; }
        public override string ToString()
        {
            return Name;
        }



    }
}
