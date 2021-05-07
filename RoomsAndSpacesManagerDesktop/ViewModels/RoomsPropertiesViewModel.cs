using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class RoomsPropertiesViewModel : ViewModel
    {
        //MainDbContext context = new MainDbContext();
        public static RoomsDbContext roomsContext = new RoomsDbContext();
        public RoomsPropertiesViewModel()
        {
            Categories = roomsContext.GetCategories();

            #region Комманды
            PushToDbCommand = new RelayCommand(OnPushToDbCommandExecutde, CanPushToDbCommandExecute);
            #endregion
        }

        #region Combobox - Список категорий
        private List<CategoryDto> categories;
        public List<CategoryDto> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        private CategoryDto selectedCategoties;
        /// <summary>
        /// Выбранная категория помещений
        /// </summary>
        public CategoryDto SelectedCategoties
        {
            get { return selectedCategoties; }
            set
            {
                Set(ref selectedCategoties, value);
                SubCategories = roomsContext.GetSubCategotyes(SelectedCategoties);
                Rooms = new List<RoomNameDto>();
            }
        }
        #endregion

        #region Combobox - список подкатегорий

        private List<SubCategoryDto> subCategories;
        public List<SubCategoryDto> SubCategories
        {
            get { return subCategories; }
            set
            {
                Set(ref subCategories, value);
            }
        }


        private SubCategoryDto selectedSubCategoties;
        /// <summary>
        /// Выбранная подкатегория помещений
        /// </summary>
        public SubCategoryDto SelectedSubCategoties
        {
            get { return selectedSubCategoties; }
            set
            {
                selectedSubCategoties = value;
                if (SelectedSubCategoties != null)
                    Rooms = roomsContext.GetRoomNames(SelectedSubCategoties);
            }
        }

        #endregion

        #region Список комнат
        private List<RoomNameDto> rooms;
        

        public List<RoomNameDto> Rooms
        {
            get { return rooms; }
            set => Set(ref rooms, value);
        }
        #endregion

        #region Комманд. Закинуть обновления пространств в БД
        public ICommand PushToDbCommand { get; set; }
        private void OnPushToDbCommandExecutde(object p)
        {
            roomsContext.SaveChanges();
            MessageBox.Show("Данные успешно загруженны в базу данных", "Статус");
        }
        private bool CanPushToDbCommandExecute(object p) => true;
        #endregion

        #region Статус выполнения
        private string status;
        public string Status 
        { 
            get => status; 
            set => Set(ref status, value); 
        }
        #endregion
    }
}
