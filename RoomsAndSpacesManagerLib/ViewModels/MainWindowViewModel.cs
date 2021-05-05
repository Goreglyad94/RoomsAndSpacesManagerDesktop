using RoomsAndSpacesManagerDataBase;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerLib.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        static RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();

        public MainWindowViewModel()
        {

        }

        private List<RoomDto> rooms = roomAndSpacesDbContext.RaSM_Rooms.ToList();
        public List<RoomDto> Rooms { get => rooms; set => rooms = value; } 
    }
}
