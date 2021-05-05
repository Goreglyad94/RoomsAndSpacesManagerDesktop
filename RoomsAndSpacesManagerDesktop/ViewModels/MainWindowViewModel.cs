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
using RoomsAndSpacesManagerDesktop.Models.CsvModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.Views.Windows;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region филды
        ProjectsDbContext context = new ProjectsDbContext();
        MainDbContext mainContext = new MainDbContext();
        #endregion

        public MainWindowViewModel()
        {
            //context.DB();
            Projects = mainContext.context.RaSM_Projects.ToList();

            #region Команды
            PushToDbCommand = new RelayCommand(OnPushToDbCommandExecutde, CanPushToDbCommandExecute);
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecutde, CanAddNewRowCommandExecute);
            AddNewProjectCommand = new RelayCommand(OnAddNewProjectCommandExecutde, CanAddNewProjectCommandExecute);
            AddNewBuildingCommand = new RelayCommand(OnAddNewBuildingCommandExecutde, CanAddNewBuildingCommandExecute);
            #endregion
        }
        /*TabControl1 - Создание нового проекта и здания~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Имена новых проекта и здания
        public string NewProjectName { get; set; }
        public string NewBuildingName { get; set; }
        #endregion

        

        #region СелектедПроджект для добавления новых проектов
        private ProjectDto selectedProjectForAdd;

        public ProjectDto SelectedProjectForAdd
        {
            get { return selectedProjectForAdd; }
            set
            {
                selectedProjectForAdd = value;
            }
        }
        #endregion

        

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
                if (SelectedProject.Buildings != null && SelectedProject.Buildings.Count != 0)
                    Buildings = context.GetModels(SelectedProject);
            }
        }
        #endregion

        #region Комманда. Создать новый проект
        public ICommand AddNewProjectCommand { get; set; }
        private void OnAddNewProjectCommandExecutde(object p)
        {
            context.AddNewProjects(new ProjectDto()
            {
                Name = NewProjectName
            });
            Projects = context.GetProjects();
        }
        private bool CanAddNewProjectCommandExecute(object p) => true;
        #endregion

        #region Комманда. Создать новую модель
        public ICommand AddNewBuildingCommand { get; set; }
        private void OnAddNewBuildingCommandExecutde(object p)
        {
            context.AddNewBuilding(new BuildingDto()
            {
                ProjectId = SelectedProject.Id,
                Name = NewBuildingName
            });

            Buildings = context.GetModels(SelectedProject);
        }
        private bool CanAddNewBuildingCommandExecute(object p) => true;
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
        private void OnPushToDbCommandExecutde(object p)
        {
            context.AddNewRooms(SelectedBuilding, Rooms.ToList());
            Rooms = new ObservableCollection<RoomDto>(context.GetRooms(SelectedBuilding));
        }
        private bool CanPushToDbCommandExecute(object p) => true;
        #endregion
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/




    }
}
