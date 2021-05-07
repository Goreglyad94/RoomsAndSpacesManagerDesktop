using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
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
        List<RoomDto> roomDtos;
        RoomsDbContext roomsContext = new RoomsDbContext();
        List<RoomNameDto> roomsNamesList;
        #endregion

        public CreateIssueViewModel()
        {
            //context.DB();
            Projects = projContext.GetProjects();
            Categories = roomsContext.GetCategories();

            #region Команды
            PushToDbCommand = new RelayCommand(OnPushToDbCommandExecutde, CanPushToDbCommandExecute);
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecutde, CanAddNewRowCommandExecute);
            AddNewProjectCommand = new RelayCommand(OnAddNewProjectCommandExecutde, CanAddNewProjectCommandExecute);
            AddNewBuildingCommand = new RelayCommand(OnAddNewBuildingCommandExecutde, CanAddNewBuildingCommandExecute);
            DeleteCommand = new RelayCommand(OnDeleteCommandExecutde, CanDeleteCommandExecute);
            RenderComboboxCommand = new RelayCommand(OnRenderComboboxCommandExecutde, CanRenderComboboxCommandExecute);
            #endregion
        }
        /*Создание нового проекта и здания~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Имена новых проекта и здания
        private string newProjectName;
        public string NewProjectName
        {
            get => newProjectName;
            set => Set(ref newProjectName, value);
        }

        private string newBuildingName;
        public string NewBuildingName
        {
            get => newBuildingName;
            set => Set(ref newBuildingName, value);
        }
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
                if (SelectedProject != null)
                {
                    if (SelectedProject.Buildings != null)
                        Buildings = projContext.GetModels(SelectedProject);
                }
                else
                {
                    Buildings = new List<BuildingDto>();
                }

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
            NewProjectName = string.Empty;
            if (Buildings != null && Buildings.Count != 0)
            {
                Buildings.Clear();
                OnPropertyChanged(nameof(Buildings));
            }

        }
        private bool CanAddNewProjectCommandExecute(object p) => true;
        #endregion

        #region Комманда. Создать новую модель
        public ICommand AddNewBuildingCommand { get; set; }
        private void OnAddNewBuildingCommandExecutde(object p)
        {
            if (p != null)
            {
                projContext.AddNewBuilding(new BuildingDto()
                {
                    ProjectId = (p as ProjectDto).Id,
                    Name = NewBuildingName
                });
                Buildings = projContext.GetModels(p as ProjectDto);
            }
            NewBuildingName = string.Empty;

        }
        private bool CanAddNewBuildingCommandExecute(object p) => true;
        #endregion

        #region Комманда удаления проектов и зданий
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
                {
                    roomDtos = projContext.GetRooms(SelectedBuilding);
                    Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                    Rooms.Refresh();
                }
                else
                    Rooms = null;
            }
        }
        #endregion

        /*Верхняя панель. Список категорий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Список категорий и подкатегорий

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
            }
        }

        #endregion

        #region Список исходных помещений
        private ICollectionView roomsNames;
        public ICollectionView RoomsNames
        {
            get => roomsNames;
            set => Set(ref roomsNames, value);
        }

        private RoomNameDto selectedRoomName;

        public RoomNameDto SelectedRoomName
        {
            get { return selectedRoomName; }
            set
            {
                selectedRoomName = value;
                AddRoomInfo();
                selectedRoomName = null;
            }
        }

        private void AddRoomInfo()
        {
            SelectedRoom.RoomNameId = SelectedRoomName.Id;
            SelectedRoom.Min_area = SelectedRoomName.Min_area;
            SelectedRoom.Class_chistoti_GMP = SelectedRoomName.Class_chistoti_GMP;
            SelectedRoom.Class_chistoti_SanPin = SelectedRoomName.Class_chistoti_SanPin;
            SelectedRoom.Class_chistoti_SP_158 = SelectedRoomName.Class_chistoti_SP_158;
            SelectedRoom.T_calc = SelectedRoomName.T_calc;
            SelectedRoom.T_max = SelectedRoomName.T_max;
            SelectedRoom.T_min = SelectedRoomName.T_min;
            SelectedRoom.Pritok = SelectedRoomName.Pritok;
            SelectedRoom.Vityazhka = SelectedRoomName.Vityazhka;
            SelectedRoom.Ot_vlazhnost = SelectedRoomName.Ot_vlazhnost;
            SelectedRoom.KEO_est_osv = SelectedRoomName.KEO_est_osv;
            SelectedRoom.KEO_sovm_osv = SelectedRoomName.KEO_sovm_osv;
            SelectedRoom.Discription_OV = SelectedRoomName.Discription_OV;
            SelectedRoom.Osveshennost_pro_obshem_osvech = SelectedRoomName.Osveshennost_pro_obshem_osvech;
            SelectedRoom.Group_el_bez = SelectedRoomName.Group_el_bez;
            SelectedRoom.Discription_EOM = SelectedRoomName.Discription_EOM;
            SelectedRoom.Discription_AR = SelectedRoomName.Discription_AR;
            SelectedRoom.Equipment_VK = SelectedRoomName.Equipment_VK;
            SelectedRoom.Discription_SS = SelectedRoomName.Discription_SS;
            SelectedRoom.Discription_AK_ATH = SelectedRoomName.Discription_AK_ATH;
            SelectedRoom.Discription_GSV = SelectedRoomName.Discription_GSV;
            SelectedRoom.Discription_HS = SelectedRoomName.Discription_HS;
        }

        #endregion

        #region Комманда при отрисовке комбобокса
        private ICommand renderComboboxCommand;
        public ICommand RenderComboboxCommand
        {
            get => renderComboboxCommand;
            set => renderComboboxCommand = value;
        }

        private void OnRenderComboboxCommandExecutde(object p)
        {
            if (p != null)
            {
                roomsNamesList = RoomsPropertiesViewModel.roomsContext.GetRoomNames(p as SubCategoryDto);
                RoomsNames = CollectionViewSource.GetDefaultView(roomsNamesList);
                RoomsNames.Refresh();
            }

        }
        private bool CanRenderComboboxCommandExecute(object p) => true;

        #endregion

        #endregion

        /*Верхняя панель. Список проектов и зданий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/







        #region Список помещений. Выбранное помещение
        private ICollectionView rooms;
        public ICollectionView Rooms
        {
            get => rooms;
            set => Set(ref rooms, value);
        }

        private static RoomDto selectedRoom;


        public static RoomDto SelectedRoom
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
                roomDtos.Add(new RoomDto()
                {
                    BuildingId = SelectedBuilding.Id
                });
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();

                //RoomsNames = CollectionViewSource.GetDefaultView(roomsNamesList);
                //RoomsNames.Refresh();
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
            projContext.AddNewRooms(SelectedBuilding, roomDtos);
            projContext.SaveChanges();
            roomDtos = projContext.GetRooms(SelectedBuilding);
            Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            Rooms.Refresh();
            MessageBox.Show("Данные успешно загруженны в базу данных", "Статус");
        }
        private bool CanPushToDbCommandExecute(object p) => true;
        #endregion
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    }
}
