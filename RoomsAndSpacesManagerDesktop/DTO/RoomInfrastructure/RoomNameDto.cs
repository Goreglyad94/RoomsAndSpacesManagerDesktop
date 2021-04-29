using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.DTO.RoomInfrastructure
{
    [Table("RaSM_RoomNames")]
    public class RoomNameDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public int SubCategotyId { get; set; }
        public virtual SubCategoryDto SubCategory { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
