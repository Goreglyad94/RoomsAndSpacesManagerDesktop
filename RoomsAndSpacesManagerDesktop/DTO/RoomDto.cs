using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using RoomsAndSpacesManagerDesktop.DTO.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;

namespace RoomsAndSpacesManagerDesktop.DTO
{
    [Table("RaSM_Rooms")]
    public class RoomDto : ViewModel
    {


        public int Id { get; set; }


        public string SelectedCategory { get; set; }
        public string SelectedSubCategory { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string RoomNumber { get; set; }
        public double Area { get; set; }

        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }
    }
}
