using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDataBase.Dto
{
    [Table("RaSM_SubdivisionDto")]
    public class SubdivisionDto : ViewModel
    {
        

        public int Id { get; set; }
        public string Name { get; set; }


        [NotMapped]
        public bool IsChecked { get; set; } = false;
        [NotMapped]
        public int? SunnuryArea { get; set; }

        private bool isReadOnly = false;
        [NotMapped]
        public bool IsReadOnly { get => isReadOnly; set => Set(ref isReadOnly, value); }


        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }

        public virtual ICollection<RoomDto> Rooms { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public SubdivisionDto()
        {

        }
        public SubdivisionDto(SubdivisionDto subdivision)
        {
            this.Name = subdivision.Name;

        }

        

    }
}
