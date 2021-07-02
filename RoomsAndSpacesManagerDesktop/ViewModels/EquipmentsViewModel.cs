using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using RoomsAndSpacesManagerDesktop.Models.ExcelModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class EquipmentsViewModel : ViewModel
    {
        public static RoomDto Room { get; set; }

        EquipmentDbContext context = new EquipmentDbContext();

        public EquipmentsViewModel()
        {

            RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetEquipments(Room));
            RoomEquipmentsList.Refresh();

            #region Комманды
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecuted, CanAddNewRowCommandExecute);
            SaveChangesCommand = new RelayCommand(OnSaveChangesCommandExecuted, CanSaveChangesCommandExecute);
            
            DeleteEquipmentCommand = new RelayCommand(OnDeleteEquipmentCommandExecuted, CanDeleteEquipmentCommandExecute); 
            #endregion
        }

        #region КоллекшенВью для списка оборудования
        private ICollectionView roomEquipmentsList;
        public ICollectionView RoomEquipmentsList
        {
            get => roomEquipmentsList;
            set => Set(ref roomEquipmentsList, value);
        }
        #endregion

        #region Комманд. Добавить новую строку
        public ICommand AddNewRowCommand { get; set; }
        private void OnAddNewRowCommandExecuted(object obj)
        {
            context.AddNewEquipment(Room);
            RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetEquipments(Room));
            RoomEquipmentsList.Refresh();
        }
        private bool CanAddNewRowCommandExecute(object obj) => true;
        #endregion

        #region Комманд. Сохранить изменения


        public ICommand SaveChangesCommand { get; set; }
        private void OnSaveChangesCommandExecuted(object obj)
        {
            context.SaveChanges();
        }
        private bool CanSaveChangesCommandExecute(object obj) => true;
        #endregion


        #region Комманд. Удалить строку оборудования

        public ICommand DeleteEquipmentCommand { get; set; }

        private void OnDeleteEquipmentCommandExecuted(object obj)
        {
            context.RemoveEquipment(obj as EquipmentDto);
            RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetEquipments(Room));
            RoomEquipmentsList.Refresh();
        }
        private bool CanDeleteEquipmentCommandExecute(object obj) => true;
        #endregion
    }
}
