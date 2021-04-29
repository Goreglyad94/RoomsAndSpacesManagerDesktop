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
using RoomsAndSpacesManagerDesktop.ViewModels.Base;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region филды
        ProjectsDbContext context = new ProjectsDbContext();

        #endregion

        public MainWindowViewModel()
        {
            //context.DB();
            Projects = context.GetProjects();

            #region Команды
            PushToDbCommand = new RelayCommand(OnDPushToDbCommandExecutde, CanPushToDbCommandExecute);
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecutde, CanAddNewRowCommandExecute);
            #endregion
        }
        /*TabControl1 - Создание нового проекта и здания~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/







        /*TabControl2 - Список проектов и таблица из бд~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region Верхняя панель. Список проектов и зданий
        #region Список проектов. Выбранный проект
        private List<ProjectDto> projects;
        /// <summary>
        /// Список проектов. Из БД
        /// </summary>
        public List<ProjectDto> Projects
        {
            get => projects;
            set => Set(ref projects, value);
        }
        private ProjectDto selectedProject;

        public ProjectDto SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                Buildings = SelectedProject.Buildings.ToList();
            }
        }
        #endregion




        #region Список зданий. (Добавить выбранное здание)
        private List<BuildingDto> buildings;
        /// <summary>
        /// Список проектов. Из БД
        /// </summary>
        public List<BuildingDto> Buildings
        {
            get => buildings;
            set => Set(ref buildings, value);
        }

        private BuildingDto selectedBuilding;

        public BuildingDto SelectedBuilding
        {
            get { return selectedBuilding; }
            set 
            {
                selectedBuilding = value;
                if (SelectedBuilding != null)
                    Rooms = new ObservableCollection<RoomDto>(context.GetRooms(SelectedBuilding));
                else
                    Rooms = null;
            }
        }


        #endregion

        #endregion


        /*TubControl2 - таблица данных~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region DataGrid - Rooms
        /// <summary>
        /// Привязка DataGrid к коллекции Rooms
        /// </summary>



        private ObservableCollection<RoomDto> rooms = new ObservableCollection<RoomDto>();
        public ObservableCollection<RoomDto> Rooms 
        { 
            get => rooms; 
            set => Set(ref rooms,value); 
        }

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

        #endregion


        /*TubControl2 - Нижняя панель. Кнопки синхронизации с БД~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region Добавить новую строку комнаты с Айдишником здания
        public ICommand AddNewRowCommand { get; set; }
        private void OnAddNewRowCommandExecutde(object p)
        {
            if (SelectedBuilding != null)
            {
                Rooms.Add(new RoomDto()
                {
                    BuildingId = SelectedBuilding.Id
                });
                OnPropertyChanged(nameof(Rooms));
            }
        }
        private bool CanAddNewRowCommandExecute(object p) => true;
        #endregion

        #region Комманд. Закинуть обновления пространств в БД
        public ICommand PushToDbCommand { get; set; }
        private void OnDPushToDbCommandExecutde(object p)
        {
            foreach (var item in Rooms)
            {
                item.Category = item.SelectedCategory?.Name;
            }
            context.AddNewRooms(SelectedBuilding, Rooms.ToList());
        }
        private bool CanPushToDbCommandExecute(object p) => true;
        #endregion


        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/




    }
}
