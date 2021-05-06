using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Data.DataBaseContext;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.CsvModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.Views.Windows;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class CreateIssueViewModel : ViewModel
    {
        #region филды
        ProjectsDbContext projContext = new ProjectsDbContext();
        #endregion

        public CreateIssueViewModel()
        {
            //context.DB();
            Projects = projContext.GetProjects();


            #region Команды
            PushToDbCommand = new RelayCommand(OnPushToDbCommandExecutde, CanPushToDbCommandExecute);
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecutde, CanAddNewRowCommandExecute);
            AddNewProjectCommand = new RelayCommand(OnAddNewProjectCommandExecutde, CanAddNewProjectCommandExecute);
            AddNewBuildingCommand = new RelayCommand(OnAddNewBuildingCommandExecutde, CanAddNewBuildingCommandExecute);
            DeleteCommand = new RelayCommand(OnDeleteCommandExecutde, CanDeleteCommandExecute);
            #endregion
        }
        /*Создание нового проекта и здания~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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



        /*Верхняя панель. Список проектов и зданий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Список проектов. Выбранный проект
        private List<ProjectDto> projects;
        /// <summary> Список проектов. Из БД </summary>
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
                if (SelectedProject != null && SelectedProject.Buildings != null && SelectedProject.Buildings.Count != 0)
                    Buildings = projContext.GetModels(SelectedProject);
            }
        }
        #endregion

        #region Комманда. Создать новый проект
        public ICommand AddNewProjectCommand { get; set; }
        private void OnAddNewProjectCommandExecutde(object p)
        {
            projContext.AddNewProjects(new ProjectDto()
            {
                Name = NewProjectName
            });
            Projects = projContext.GetProjects();
        }
        private bool CanAddNewProjectCommandExecute(object p) => true;
        #endregion

        #region Комманда. Создать новую модель
        public ICommand AddNewBuildingCommand { get; set; }
        private void OnAddNewBuildingCommandExecutde(object p)
        {
            projContext.AddNewBuilding(new BuildingDto()
            {
                ProjectId = SelectedProject.Id,
                Name = NewBuildingName
            });

            Buildings = projContext.GetModels(SelectedProject);
        }
        private bool CanAddNewBuildingCommandExecute(object p) => true;
        #endregion

        #region Комманда
        public ICommand DeleteCommand { get; set; }
        private void OnDeleteCommandExecutde(object p)
        {
            if (p is ProjectDto)
            {
                projContext.RemoveProject(p as ProjectDto);
                Projects = projContext.GetProjects();
            }
            if (p is BuildingDto)
            {
                projContext.RemoveBuilding(p as BuildingDto);
                Buildings = projContext.GetModels(SelectedProject);
            }
            
        }
        private bool CanDeleteCommandExecute(object p) => true;
        #endregion

        #region Список зданий
        private List<BuildingDto> buildings;
        /// <summary> Список проектов. Из БД </summary>
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
                    Rooms = new ObservableCollection<RoomDto>(projContext.GetRooms(SelectedBuilding));
                else
                    Rooms = null;
            }
        }
        #endregion

        /*DataGrid помещений~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


        #region Список помещений. Выбранное помещение
        private ObservableCollection<RoomDto> rooms = new ObservableCollection<RoomDto>();
        /// <summary> Привязка DataGrid к коллекции Rooms </summary>
        public ObservableCollection<RoomDto> Rooms 
        { 
            get => rooms; 
            set => Set(ref rooms,value); 
        }

        
        private RoomDto selectedRoom;
        

        /// <summary> SelectedItem DataGrid Rooms </summary>
        public RoomDto SelectedRoom
        {
            get => selectedRoom;
            set
            {
                selectedRoom = value;
            }
        }

        #endregion


        /*Нижняя панель. Интерфейс добавления строки и синхронизации с БД~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region Добавить новую строку комнаты с Айдишником здания
        public ICommand AddNewRowCommand { get; set; }

        /// <summary>
        /// Метод. Добавть новую строку
        /// </summary>
        /// <param name="p"></param>
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
        private bool CanAddNewRowCommandExecute(object p) 
        {
            if (SelectedBuilding == null) return false;
            else return true;
        }
        #endregion

        #region Комманд. Закинуть обновления пространств в БД
        public ICommand PushToDbCommand { get; set; }
        private void OnPushToDbCommandExecutde(object p)
        {
            projContext.AddNewRooms(SelectedBuilding, Rooms.ToList());
            Rooms = new ObservableCollection<RoomDto>(projContext.GetRooms(SelectedBuilding));
        }
        private bool CanPushToDbCommandExecute(object p) => true;
        #endregion
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    }
}
