using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomsAndSpacesManagerDesktop.DTO
{
    [Table("RaSM_Projects")]
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BuildingDto> Buildings { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
