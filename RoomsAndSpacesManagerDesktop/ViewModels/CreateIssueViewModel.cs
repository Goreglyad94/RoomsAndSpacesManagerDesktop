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
using RoomsAndSpacesManagerDesktop.Models.ExcelModels;
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
        UploadToCsvModel uploadToCsvModel = new UploadToCsvModel();
        List<RoomNameDto> roomsNamesList;
        #endregion

        public CreateIssueViewModel()
        {
            

            //context.DB();
            Projects = projContext.GetProjects();
            Categories = roomsContext.GetCategories();

            #region Команды
            PushToDbCommand = new RelayCommand(OnPushToDbCommandExecutde, CanPushToDbCommandExecute);
            PullFromDbCommand = new RelayCommand(OnPullFromDbCommandExecutde, CanPullFromDbCommandExecute);
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecutde, CanAddNewRowCommandExecute);
            AddNewProjectCommand = new RelayCommand(OnAddNewProjectCommandExecutde, CanAddNewProjectCommandExecute);
            AddNewBuildingCommand = new RelayCommand(OnAddNewBuildingCommandExecutde, CanAddNewBuildingCommandExecute);
            DeleteCommand = new RelayCommand(OnDeleteCommandExecutde, CanDeleteCommandExecute);
            DeleteIssueCommand = new RelayCommand(OnDeleteIssueCommandExecutde, CanDeleteIssueCommandExecute);
            SetDefaultValueCommand = new RelayCommand(OnSetDefaultValueCommandExecutde, CanSetDefaultValueCommandExecute);
            AddNewSubdivisionCommand = new RelayCommand(OnAddNewSubdivisionCommandExecutde, CanAddNewSubdivisionCommandExecute);
            RenderComboboxCommand = new RelayCommand(OnRenderComboboxCommandExecutde, CanRenderComboboxCommandExecute);
            LoadedCommand = new RelayCommand(OnLoadedCommandExecutde, CanLoadedCommandExecute);
            CopySubdivisionCommnd = new RelayCommand(OnCopySubdivisionCommndExecutde, CanCopySubdivisionCommndExecute);
            LoadedSummuryCommand = new RelayCommand(OnLoadedSummuryCommandExecutde, CanLoadedSummuryCommandExecute);
            UploadProgramToCsv = new RelayCommand(OnUploadProgramToCsvExecutde, CanUploadProgramToCsvExecute);
            #endregion
        }

        /*MainWindow~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Команда рендера окна
        public ICommand LoadedCommand { get; set; }
        private void OnLoadedCommandExecutde(object obj)
        {
            if (SelectedSubdivision != null)
            {
                roomDtos = projContext.GetRooms(SelectedSubdivision);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }
            

        }

        private bool CanLoadedCommandExecute(object obj) => true;
        #endregion

        /*Создание нового проекта и здания~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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

        private string newSubdivisionName;
        public string NewSubdivisionName
        {
            get => newSubdivisionName;
            set => Set(ref newSubdivisionName, value);
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

        /*Верхняя панель. Список проектов и зданий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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

                    AllRooms = projContext.GetAllRoomsByProject(SelectedProject).OrderBy(x => x.Subdivision.BuildingId).ToList();
                }
                else
                {
                    Buildings = new List<BuildingDto>();
                }

            }
        }
        #endregion

        #region Список зданий. Выбранное здание
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
                    if (SelectedBuilding.Subdivisions != null)
                        Subdivisions = projContext.GetSubdivisions(SelectedBuilding);
                }
                else
                    Subdivisions = null;
            }
        }
        #endregion

        #region Список подразделений. Выбранное подразделение
        private List<SubdivisionDto> subdivisions;
        /// <summary> Список проектов. Из БД </summary>
        public List<SubdivisionDto> Subdivisions
        {
            get => subdivisions;
            set => Set(ref subdivisions, value);
        }

        private SubdivisionDto selectedSubdivision;

        public SubdivisionDto SelectedSubdivision
        {
            get { return selectedSubdivision; }
            set
            {
                selectedSubdivision = value;
                if (SelectedSubdivision != null)
                {
                    roomDtos = projContext.GetRooms(SelectedSubdivision);
                    Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                    Rooms.Refresh();
                }
                else
                    Rooms = null;
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
        private bool CanAddNewBuildingCommandExecute(object p)
        {
            if (p != null)
            {
                return true;
            }
            else { return false; }
        }
        #endregion

        #region Комманда. Создать новое подразделение
        public ICommand AddNewSubdivisionCommand { get; set; }
        private void OnAddNewSubdivisionCommandExecutde(object p)
        {
            if (p != null)
            {
                projContext.AddNewSubdivision(new SubdivisionDto()
                {
                    BuildingId = (p as BuildingDto).Id,
                    Name = NewSubdivisionName
                });
                Subdivisions = projContext.GetSubdivisions(p as BuildingDto);
            }
            NewBuildingName = string.Empty;

        }
        private bool CanAddNewSubdivisionCommandExecute(object p)
        {
            if (p != null)
            {
                return true;
            }
            else { return false; }
        }
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
            if (p is SubdivisionDto)
            {
                projContext.RemoveSubDivision(p as SubdivisionDto);
                Subdivisions = projContext.GetSubdivisions(SelectedBuilding);
            }

        }
        private bool CanDeleteCommandExecute(object p) => true;
        #endregion

        /*Верхняя панель. Список категорий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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

        /*Центральная панель. Список проектов и зданий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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

        #region Перенос помещения из одного подразделения в другое
        private SubdivisionDto selectedSubdivisionAction;

        public SubdivisionDto SelectedSubdivisionAction
        {
            get { return selectedSubdivisionAction; }
            set
            {
                selectedSubdivisionAction = value;
                if (SelectedSubdivisionAction != null)
                {
                    SelectedRoom.Subdivision = SelectedSubdivisionAction;
                    SelectedRoom.SubdivisionId = SelectedSubdivisionAction.Id;
                    projContext.SaveChanges();

                    roomDtos = projContext.GetRooms(SelectedSubdivision);
                    Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                    Rooms.Refresh();
                    SelectedSubdivisionAction = null;
                }
            }
        }

        #endregion

        #region Комманд. Удаление строк из списка
        public ICommand DeleteIssueCommand { get; set; }
        private void OnDeleteIssueCommandExecutde(object p)
        {
            if ((p as RoomDto).Id == default)
            {
                roomDtos.Remove(p as RoomDto);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }
            else
            {
                projContext.RemoveRoom(p as RoomDto);

                roomDtos = projContext.GetRooms(SelectedSubdivision);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }
        }
        private bool CanDeleteIssueCommandExecute(object p) => true;

        #endregion

        #region Получить значение по умолчанию по выбранной строке
        public ICommand SetDefaultValueCommand { get; set; }

        private void OnSetDefaultValueCommandExecutde(object p)
        {

            RoomDto room = p as RoomDto;

            if (room.Name != null)
            {
                room.RoomNameId = room.RoomName.Id;
                room.Min_area = room.RoomName.Min_area;
                room.Class_chistoti_GMP = room.RoomName.Class_chistoti_GMP;
                room.Class_chistoti_SanPin = room.RoomName.Class_chistoti_SanPin;
                room.Class_chistoti_SP_158 = room.RoomName.Class_chistoti_SP_158;
                room.T_calc = room.RoomName.T_calc;
                room.T_max = room.RoomName.T_max;
                room.T_min = room.RoomName.T_min;
                room.Pritok = room.RoomName.Pritok;
                room.Vityazhka = room.RoomName.Vityazhka;
                room.Ot_vlazhnost = room.RoomName.Ot_vlazhnost;
                room.KEO_est_osv = room.RoomName.KEO_est_osv;
                room.KEO_sovm_osv = room.RoomName.KEO_sovm_osv;
                room.Discription_OV = room.RoomName.Discription_OV;
                room.Osveshennost_pro_obshem_osvech = room.RoomName.Osveshennost_pro_obshem_osvech;
                room.Group_el_bez = room.RoomName.Group_el_bez;
                room.Discription_EOM = room.RoomName.Discription_EOM;
                room.Discription_AR = room.RoomName.Discription_AR;
                room.Equipment_VK = room.RoomName.Equipment_VK;
                room.Discription_SS = room.RoomName.Discription_SS;
                room.Discription_AK_ATH = room.RoomName.Discription_AK_ATH;
                room.Discription_GSV = room.RoomName.Discription_GSV;
                room.Discription_HS = room.RoomName.Discription_HS;

                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValueCommandExecute(object p) => true;
        #endregion

        /*Нижняя панель. Интерфейс добавления строки и синхронизации с БД~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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
                    SubdivisionId = SelectedSubdivision.Id
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

        #region Копировать подразделение из другого здания
        public ICommand CopySubdivisionCommnd { get; set; }



        public static List<SubdivisionDto> CopySubdivisionList { get; set; }
        /// <summary>
        /// Метод. Добавть новую строку
        /// </summary>
        /// <param name="p"></param>
        private void OnCopySubdivisionCommndExecutde(object p)
        {
            CopySubDivisionViewModel.projContext = projContext;
            CopySubDivisionViewModel.selectedBuildingId = SelectedBuilding.Id;
            CopySubdivisionWindow copySubdivisionWindow = new CopySubdivisionWindow();
            CopySubDivisionViewModel copySubDivisionViewModel = new CopySubDivisionViewModel();
            copySubDivisionViewModel.copySubdivisionWindow = copySubdivisionWindow;
            copySubdivisionWindow.DataContext = copySubDivisionViewModel;
            copySubdivisionWindow.ShowDialog();

            Subdivisions = projContext.GetSubdivisions(SelectedBuilding);
        }
        private bool CanCopySubdivisionCommndExecute(object p)
        {
            if (SelectedBuilding == null) return false;

            return true;
        }
        #endregion

        #region Комманд. Закинуть обновления пространств в БД
        public ICommand PushToDbCommand { get; set; }
        private void OnPushToDbCommandExecutde(object p)
        {
            projContext.AddNewRooms(roomDtos);
            projContext.SaveChanges();
            roomDtos = projContext.GetRooms(SelectedSubdivision);
            Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            Rooms.Refresh();
            MessageBox.Show("Данные успешно загруженны в базу данных", "Статус");
        }
        private bool CanPushToDbCommandExecute(object p) => true;
        #endregion

        #region Комманд. Получить обновления пространств из БД
        public ICommand PullFromDbCommand { get; set; }
        private void OnPullFromDbCommandExecutde(object p)
        {
            MainExcelModel mainExcelModel = new MainExcelModel();
        }
        private bool CanPullFromDbCommandExecute(object p) => true;
        #endregion

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /*ViewModel для таблицы "Программа помещений"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Список помещений для целого проекта с сортировкой. Помещения получаются при выборе проекта. 
        private List<RoomDto> allRooms;
        

        public List<RoomDto> AllRooms
        {
            get { return allRooms; }
            set { Set(ref allRooms, value); }
        }
        #endregion



        /*Таблица "Сводная"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


        #region Список всех помещений для проекта
        private List<BuildingDto> _summury;
        public List<BuildingDto> _Summury
        {
            get => _summury;
            set => _summury = value;
        }

        private ICollectionView summury;
        public ICollectionView Summury
        {
            get => summury;
            set => Set(ref summury, value);
        } 
        #endregion

        #region Комманд. Загрузка окна Summury

        public ICommand LoadedSummuryCommand { get; set; }
        private void OnLoadedSummuryCommandExecutde(object obj)
        {
            int? sss = 0;
            if (SelectedProject != null)
            {
                List<BuildingDto> buildList = projContext.GetModels(SelectedProject);
                foreach (var build in buildList)
                {
                    int? summAreaBuild = 0;
                    foreach (var subDiv in build.Subdivisions)
                    {
                        int? summAreaSubdiv = 0;
                        foreach (var room in subDiv.Rooms)
                        {
                            if (room.Min_area != null)
                            {
                                int i;
                                int.TryParse(room.Min_area, out i);
                                summAreaSubdiv += i;
                            }
                                
                        }
                        subDiv.SunnuryArea = summAreaSubdiv;
                        summAreaBuild += summAreaSubdiv;
                    }
                    build.SunnuryArea = summAreaBuild;
                    sss += summAreaBuild;
                }
                Summury = CollectionViewSource.GetDefaultView(buildList);
                Summury.Refresh();
                SummuryArea = sss;
            }    
        }
        private bool CanLoadedSummuryCommandExecute(object obj) => true;

        #endregion

        #region Итоговая площадь
        private int? summuryArea;
        public int? SummuryArea
        {
            get { return summuryArea; }
            set { Set(ref summuryArea, value); }
        }
        #endregion

        #region Комманд. Выгрузка в Excel

        public ICommand UploadProgramToCsv { get; set; }
        private void OnUploadProgramToCsvExecutde(object obj)
        {
            try
            {
                uploadToCsvModel.UploadRoomProgramToExcel(SelectedProject);
                MessageBox.Show("Выгрузка завершена", "Статус");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            
        }
        private bool CanUploadProgramToCsvExecute(object obj) => true;

        #endregion

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


    }
}
