using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class RoomEquipmentsViewModel : ViewModel
    {
        RoomsDbContext context = new RoomsDbContext();
        public RoomEquipmentsViewModel()
        {
            RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetAllEquipments());
            RoomEquipmentsList.Refresh();
        }

        public ICollectionView RoomEquipmentsList { get; set; }
    }
}
