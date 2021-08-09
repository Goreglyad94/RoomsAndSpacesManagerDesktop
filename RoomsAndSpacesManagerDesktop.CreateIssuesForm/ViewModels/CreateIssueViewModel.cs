using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;
using RoomsAndSpacesManagerDataBase.Dto;
using System.ComponentModel;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.ExcelModels;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Mediators;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.UploadModels;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels
{
    internal class CreateIssueViewModel : ViewModel
    {
        #region филды
        ProjectsDbContext projContext = new ProjectsDbContext();
        private List<RoomDto> roomDtos;
        RoomsDbContext roomsContext = new RoomsDbContext();
        //UploadToCsvModel uploadToCsvModel = new UploadToCsvModel();
        List<RoomNameDto> roomsNamesList;
        IUploadService uploadService;
        ProjectDto SelectionProject;
        private SubdivisionDto SelectedSubdivision { get; set; }

        ISqlRequestService sqlRequestService;
        #endregion
        public CreateIssueViewModel(ISqlRequestService _sqlRequestService)
        {
            sqlRequestService = _sqlRequestService;
            #region Медиаторы
            Mediator.Register("ThrowSubdivision", OnChangeView);
            Mediator.Register("Project", OnChangeView);
            Mediator.Register("ThrowDivision", OnChangeColumnDatagridBySelectedDivision);
            Mediator.Register("ThrowSubCategories", OnSelectCategories);
            Mediator.Register("AddNewRow", OnAddNewRow);
            Mediator.Register("SaveChanges", OnSaveChanges);
            Mediator.Register("CopySubdivisios", OnCopySubdivisios);
            Mediator.Register("SelectDivision", OnSelectDivision);
            Mediator.Register("UploadProgramAndSummaryToExcel", UploadProgramAndSummaryToExcel);
            Mediator.Register("ThrowProjectOnCreateIssueViewModel", OnGetProject);
            #endregion

            uploadService = new UploadMainModel();

            #region Команды

            DeleteIssueCommand = new RelayCommand(OnDeleteIssueCommandExecutde, CanDeleteIssueCommandExecute);
            SetDefaultValueCommand = new RelayCommand(OnSetDefaultValueCommandExecutde, CanSetDefaultValueCommandExecute);
            RenderComboboxCommand = new RelayCommand(OnRenderComboboxCommandExecutde, CanRenderComboboxCommandExecute);
            LoadedCommand = new RelayCommand(OnLoadedCommandExecutde, CanLoadedCommandExecute);
            LoadedSummuryCommand = new RelayCommand(OnLoadedSummuryCommandExecutde, CanLoadedSummuryCommandExecute);
            UploadProgramToCsv = new RelayCommand(OnUploadProgramToCsvExecutde, CanUploadProgramToCsvExecute);
            ClearTextboxCommand = new RelayCommand(OnClearTextboxCommandExecuted, CanClearTextboxCommandExecute);
            GetEquipmentCommand = new RelayCommand(OnGetEquipmentCommandExecutde, CanGetEquipmentCommandExecute);
            //PushToDbSaveChangesCommand = new RelayCommand(OnPushToDbSaveChangesCommandExecutde, CanPushToDbSaveChangesCommandExecute);

            #endregion
        }

        #region Получить Сабдивижен из ВьюМодели проектов
        public void OnChangeView(object obj)
        {
            SelectedSubdivision = obj as SubdivisionDto;
            
            roomDtos = projContext.GetRooms(SelectedSubdivision);
            Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            Rooms.Refresh();
        }

        #endregion

        #region Получить раздел из TabContol. Скрыть столбцы в зависимости от выбранного раздела

        public void OnChangeColumnDatagridBySelectedDivision(object obj)
        {
            MessageBox.Show((string)obj);
        }

        #endregion


        #region Получить выбранный проект
        private void OnGetProject(object obj)
        {
            SelectionProject = obj as ProjectDto;
        }
        #endregion

        /*MainWindow~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Команда рендера окна

        public ICommand LoadedCommand { get; set; }
        private async void OnLoadedCommandExecutde(object obj)
        {
            allRoomNames = await roomsContext.GetAllRoomNamesAsync();
            if (SelectedSubdivision != null)
            {
                roomDtos = projContext.GetRooms(SelectedSubdivision);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }
        }

        private bool CanLoadedCommandExecute(object obj) => true;

        #endregion

        /*Центральная. Список категорий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Медиатор. Получение выбранной подкатегории

        private SubCategoryDto selectedSubcategories;
        private void OnSelectCategories(object obj)
        {
            selectedSubcategories = obj as SubCategoryDto;
        } 

        #endregion

        #region Список исходных помещений (для выбора в комбобоксе при редактировании строки). Выбранное исходное помещение. Присываивание новому помещению нового списва оборудования

        List<RoomNameDto> allRoomNames { get; set; }

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
                SelectedRoom.SetNewRoomProperties(SelectedRoomName);

                EquipmentDbContext equipmentDbContext = new EquipmentDbContext();
                if (SelectedRoom.Id != 0)
                {
                    equipmentDbContext.RemoveAllEquipment(SelectedRoom);
                    List<EquipmentDto> equipment = equipmentDbContext.GetEquipments(SelectedRoomName).Select(x => new EquipmentDto(x) { RoomId = SelectedRoom.Id }).ToList();
                    equipmentDbContext.AddNewEquipments(equipment, SelectedRoom);
                }

                selectedRoomName = null;
            }
        }

        #region Флаг разворачивания Comboboxa

        private bool isDropDownOpen;

        public bool IsDropDownOpen
        {
            get { return isDropDownOpen; }
            set
            {
                Set(ref isDropDownOpen, value);
            }
        }

        #endregion

        #endregion

        #region Комманд. Отрисовке комбобокса

        public ICommand RenderComboboxCommand { get; set; }
        private void OnRenderComboboxCommandExecutde(object p)
        {

            if (selectedSubcategories != null)
            {
                roomsNamesList = roomsContext.GetRoomNames(selectedSubcategories as SubCategoryDto);
                RoomsNames = CollectionViewSource.GetDefaultView(roomsNamesList);
                RoomsNames.Refresh();
            }

            if (RoomNameFiltering != "")
            {
                RoomsNames = CollectionViewSource.GetDefaultView(allRoomNames);
                RoomsNames.Filter = delegate (object item)
                {
                    RoomNameDto user = item as RoomNameDto;
                    if (user != null && user.Name.ToLower().StartsWith(RoomNameFiltering.ToLower())) return true;
                    return false;
                };
                RoomsNames.Refresh();
            }
        }
        private bool CanRenderComboboxCommandExecute(object p) => true;

        #endregion

        #region Строки фильтрации помещений
        private string roomNameFiltering = "";
        public string RoomNameFiltering
        {
            get { return roomNameFiltering; }
            set
            {
                roomNameFiltering = value;
                if (RoomNameFiltering != "")
                {
                    IsDropDownOpen = true;
                }
                else
                {
                    IsDropDownOpen = false;
                }
                CollectionViewSource.GetDefaultView(allRoomNames).Refresh();
            }
        }

        #endregion

        #region Комманд. Анлоадед текстбокс

        public ICommand ClearTextboxCommand { get; set; }
        private void OnClearTextboxCommandExecuted(object obj)
        {
            RoomNameFiltering = "";
        }
        private bool CanClearTextboxCommandExecute(object obj) => true;

        #endregion

        /*Центральная панель. Список помещений~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Список помещений. Выбранное помещение

        private ICollectionView rooms;
        public ICollectionView Rooms
        {
            get => rooms;
            set => Set(ref rooms, value);
        }

        private RoomDto selectedRoom;
        public RoomDto SelectedRoom
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

        #region Комманд. Получить значение по умолчанию по выбранной строке
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

        #region Комманд. Открыть окно с оборудованием

        public ICommand GetEquipmentCommand { get; set; }

        private void OnGetEquipmentCommandExecutde(object p)
        {
            //EquipmentsViewModel.Room = SelectedRoom;

            //EquipmentsWindow equipmentsWindow = new EquipmentsWindow();
            //EquipmentsViewModel vm = new EquipmentsViewModel();
            //equipmentsWindow.DataContext = vm;

            //equipmentsWindow.ShowDialog();


        }
        private bool CanGetEquipmentCommandExecute(object p) => true;


        #endregion

        #region Медиатор. Сокрытие столбцов для показа выбранного раздела

        private void OnSelectDivision(object obj)
        {

        }

        #endregion

        #region Пропсы. Визабили стобцов

        private bool min_area_vis = false;
        public bool Min_area_vis
        {
            get { return min_area_vis; }
            set 
            { 
                Set(ref min_area_vis, value); 
            }
        }

        #endregion

        /*Нижняя панель. Медиаторы~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Медиатор. Добавить новую строку комнаты с Айдишником здания
        public async void OnAddNewRow(object obj)
        {
            if (SelectedSubdivision != null)
            {
                roomDtos = projContext.AddNewRoom(SelectedSubdivision);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }
        }
        #endregion

        #region Медиатор. Копировать подразделение из другого здания

        private void OnCopySubdivisios(object obj)
        {
            MessageBox.Show("Попытка скопирвоать подразделение!");
        }

        #endregion

        #region Медиатор. Закинуть обновления пространств в БД

        public void OnSaveChanges(object obj)
        {
            projContext.SaveChanges();
        }

        #endregion

        #region Медиатор. Выгрузка в Эксель

        private void UploadProgramAndSummaryToExcel(object obj)
        {
            //sqlRequestService.GetSqlResponse();
            //MainExcelModel.CreateXslxProgramAndSummary(projContext.GetRooms(SelectedSubdivision));
            uploadService.UploadAllUssues(SelectionProject.Id, SelectionProject.Name);
            uploadService.UploadRoomProgram(SelectionProject.Id, SelectionProject.Name);
        }
 
        #endregion

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /*ViewModel для таблицы "Программа помещений"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Список помещений для целого проекта с сортировкой. Помещения получаются при выборе проекта. 

        //private List<RoomDto> allRooms;


        //public List<RoomDto> AllRooms
        //{
        //    get { return allRooms; }
        //    set { Set(ref allRooms, value); }
        //}
        //#endregion

        //#region Комманд. Сохранить изменения в номерах помещений

        //public ICommand PushToDbSaveChangesCommand { get; set; }

        //private void OnPushToDbSaveChangesCommandExecutde(object obj)
        //{
        //    projContext.SaveChanges();
        //}
        //private bool CanPushToDbSaveChangesCommandExecute(object obj)
        //{
        //    if (AllRooms != null && AllRooms.Count != 0) return true;
        //    else return false;
        //}

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
        private List<BuildingDto> buildList;
        public ICommand LoadedSummuryCommand { get; set; }
        private void OnLoadedSummuryCommandExecutde(object obj)
        {
            //int? sss = 0;
            //if (SelectedProject != null)
            //{
            //    buildList = projContext.GetModels(SelectedProject);
            //    foreach (var build in buildList)
            //    {
            //        int? summAreaBuild = 0;
            //        foreach (var subDiv in build.Subdivisions)
            //        {
            //            int? summAreaSubdiv = 0;
            //            foreach (var room in subDiv.Rooms)
            //            {
            //                if (room.Min_area != null)
            //                {
            //                    int i;
            //                    int.TryParse(room.Min_area, out i);
            //                    summAreaSubdiv += i;
            //                }

            //            }
            //            subDiv.SunnuryArea = summAreaSubdiv;
            //            summAreaBuild += summAreaSubdiv;
            //        }
            //        build.SunnuryArea = summAreaBuild;
            //        sss += summAreaBuild;
            //    }
            //    Summury = CollectionViewSource.GetDefaultView(buildList);
            //    Summury.Refresh();
            //    SummuryArea = sss;
            //}
        }
        private bool CanLoadedSummuryCommandExecute(object obj) => false;

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
                //UploadToCsvModel UploadToCsvModel = new UploadToCsvModel(SelectedProject, buildList, Koef);
                //UploadToCsvModel.UploadToExcel();
                //uploadToCsvModel.UploadRoomProgramToExcel(SelectedProject);
                //uploadToCsvModel.UploadRoomSummaryToExcel(buildList);
                MessageBox.Show("Выгрузка завершена", "Статус");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }

        }
        private bool CanUploadProgramToCsvExecute(object obj) => true;

        #endregion

        #region Коэффициент умножения площади

        private double koef = 2.5;

        public double Koef
        {
            get { return koef; }
            set { koef = value; }
        }


        #endregion

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    }
}
