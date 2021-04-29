using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RoomsAndSpacesManagerDesktop.Data.DataBaseContext;
using RoomsAndSpacesManagerDesktop.DTO;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.XlsModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region филды

        ProjectsDbContext projectsDbContext = new ProjectsDbContext();
        ObservableCollection<string> timesubCategoriesList;
        MainXlsModel mainXlsModel = new MainXlsModel();

        #endregion

        public MainWindowViewModel()
        {

            //mainXlsModel.GetCategoryes();

            timesubCategoriesList = new ObservableCollection<string>() { "1.1", "1.2", "1.3", "2.2", "2.2" };



            #region Команды
            AddNewProjectAndBildingCommand = new RelayCommand(OnAddNewProjectAndBildingCommandExecutde, CanAddNewProjectAndBildingCommandExecute);
            #endregion
        }

        /*TubControl1 - Создание нового проекта и здания~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region Команды
        #region Создать новый проект и здание в БД
        public ICommand AddNewProjectAndBildingCommand { get; set; }

        /// <summary> Создать новый проект в бд </summary>
        private void OnAddNewProjectAndBildingCommandExecutde(object p)
        {
            projectsDbContext.AddProject(new ProjectDto()
            {
                Name = NewProjectName
            });
        }

        private bool CanAddNewProjectAndBildingCommandExecute(object p) => true;
        #endregion
        #endregion

        #region TextBox - имя нового проекта и имя нового здания

        /// <summary> Имя нового проекта </summary>
        public string NewProjectName { get; set; }
        /// <summary> Имя нового здания </summary>
        public string NewBildingName { get; set; }
        #endregion
        /*TubControl2 - таблица данных~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region DataGrid - Rooms
        /// <summary>
        /// Привязка DataGrid к коллекции Rooms
        /// </summary>
        public ObservableCollection<RoomDto> Rooms { get; set; } = new ObservableCollection<RoomDto>();

        #region Выбранная комната из DataGrid Rooms
        private RoomDto selectedRoom;
        /// <summary>
        /// SelectedItem DataGrid Rooms
        /// </summary>
        public RoomDto SelectedRoom
        {
            get => selectedRoom;
            set 
            { 
                selectedRoom = value; 

            }
        }
        #endregion


        #region Старый код. Для работы с DataGridComboBoxColumn

        //#region Combobox - список категорий
        ///// <summary>
        ///// Список категорий (взять из БД)
        ///// </summary>
        //private ObservableCollection<string> categoriesList = new ObservableCollection<string>()
        //{
        //    "1",
        //    "2",
        //    "3"
        //};
        ///// <summary>
        ///// Список категорий (взять из БД)
        ///// </summary>
        //public ObservableCollection<string> CategoriesList
        //{
        //    get => categoriesList;
        //    set => categoriesList = value;
        //}

        //private string selectedCategory;
        ///// <summary>
        ///// Выбранная категория 
        ///// </summary>
        //public string SelectedCategory
        //{
        //    get => selectedCategory;
        //    set 
        //    { 
        //        selectedCategory = value;
        //        GetSubCats();
        //    }
        //}
        //#endregion

        //#region Combobox - список подкатегорий
        //private ObservableCollection<string> subCategoriesList;
        ///// <summary>
        ///// Список подкатегорий (взять из БД)
        ///// </summary>
        //public ObservableCollection<string> SubCategoriesList
        //{
        //    get => subCategoriesList;
        //    set => Set(ref subCategoriesList, value);
        //}


        //private string selectedSubCategory;
        ///// <summary>
        ///// Выбранная подкатегория 
        ///// </summary>
        //public string SelectedSubCategory
        //{
        //    get => selectedSubCategory;
        //    set => selectedSubCategory = value;
        //}
        //#endregion

        //#region Combobox - список названий помещений
        ///// <summary>
        ///// Список названий помещений (взять из БД)
        ///// </summary>
        //private ObservableCollection<string> roomNamesList = new ObservableCollection<string>() { "subddd", "sub112333" };
        ///// <summary>
        ///// Список названий помещений (взять из БД)
        ///// </summary>
        //public ObservableCollection<string> RoomNamesList
        //{
        //    get => roomNamesList;
        //    set => roomNamesList = value;
        //}
        //#endregion

        //#region Методы
        //private void GetSubCats()
        //{
        //    if (SelectedRoom.SelectedCategory != null)
        //        SubCategoriesList = new ObservableCollection<string>(timesubCategoriesList.Where(x => x.Contains(SelectedRoom.SelectedCategory)));
        //}
        //#endregion

        #endregion

        #endregion
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    }
}
