using Autodesk.Revit.DB;
using RoomAndSpacesOV.Dto;
using RoomAndSpacesOV.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RoomAndSpacesOV.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public static List<SpaceDto> SpaciesList { get; set; }

        public MainWindowViewModel()
        {
            SpacesList = CollectionViewSource.GetDefaultView(SpaciesList);
            SpacesList.Refresh();
        }

        private ICollectionView spacesList;
        public ICollectionView SpacesList
        {
            get => spacesList;
            set => Set(ref spacesList, value);
        }


    }
}
