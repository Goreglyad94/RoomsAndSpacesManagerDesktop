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


        private SubdivisionDto SelectedSubdivision { get; set; }
        #endregion

        public CreateIssueViewModel()
        {
            #region Медиаторы
            Mediator.Register("ThrowSubdivision", OnChangeView);
            Mediator.Register("ThrowDivision", OnChangeColumnDatagridBySelectedDivision);
            Mediator.Register("AddNewRow", OnAddNewRow);
            Mediator.Register("SaveChanges", OnSaveChanges);
            Mediator.Register("CopySubdivisios", OnCopySubdivisios);
            Mediator.Register("SelectDivision", OnSelectDivision);
            #endregion


            allRoomNames = roomsContext.GetAllRoomNames();
            Categories = roomsContext.GetCategories();

            #region Команды

            //PushToDbCommand = new RelayCommand(OnPushToDbCommandExecutde, CanPushToDbCommandExecute);
            PullFromDbCommand = new RelayCommand(OnPullFromDbCommandExecutde, CanPullFromDbCommandExecute);
            //AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecutde, CanAddNewRowCommandExecute);
            DeleteIssueCommand = new RelayCommand(OnDeleteIssueCommandExecutde, CanDeleteIssueCommandExecute);
            SetDefaultValueCommand = new RelayCommand(OnSetDefaultValueCommandExecutde, CanSetDefaultValueCommandExecute);
            RenderComboboxCommand = new RelayCommand(OnRenderComboboxCommandExecutde, CanRenderComboboxCommandExecute);
            LoadedCommand = new RelayCommand(OnLoadedCommandExecutde, CanLoadedCommandExecute);
            //CopySubdivisionCommnd = new RelayCommand(OnCopySubdivisionCommndExecutde, CanCopySubdivisionCommndExecute);
            LoadedSummuryCommand = new RelayCommand(OnLoadedSummuryCommandExecutde, CanLoadedSummuryCommandExecute);
            UploadProgramToCsv = new RelayCommand(OnUploadProgramToCsvExecutde, CanUploadProgramToCsvExecute);
            ClearTextboxCommand = new RelayCommand(OnClearTextboxCommandExecuted, CanClearTextboxCommandExecute);
            GetEquipmentCommand = new RelayCommand(OnGetEquipmentCommandExecutde, CanGetEquipmentCommandExecute);
            PushToDbSaveChangesCommand = new RelayCommand(OnPushToDbSaveChangesCommandExecutde, CanPushToDbSaveChangesCommandExecute);

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

        /*MainWindow~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Команда рендера окна +
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

        /*Верхняя панель. Список категорий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Combobox - Список категорий
        private List<CategoryDto> categories;
        public List<CategoryDto> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        private CategoryDto selectedCategoties;
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
                AddRoomInfo();
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
                roomsNamesList = roomsContext.GetRoomNames(p as SubCategoryDto);
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

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(RoomNameFiltering))
                return true;
            else
                return ((item as RoomNameDto).Name.IndexOf(RoomNameFiltering, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        #endregion

        #region Анлоадед текстбокс

        public ICommand ClearTextboxCommand { get; set; }
        private void OnClearTextboxCommandExecuted(object obj)
        {
            RoomNameFiltering = "";
        }
        private bool CanClearTextboxCommandExecute(object obj) => true;

        #endregion

        /*Центральная панель. Список проектов и зданий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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

        /*Нижняя панель. Интерфейс добавления строки и синхронизации с БД~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Добавить новую строку комнаты с Айдишником здания

        public async void OnAddNewRow(object obj)
        {
            if (SelectedSubdivision != null)
            {
                roomDtos = projContext.AddNewRoom(SelectedSubdivision);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }
        }

        //public ICommand AddNewRowCommand { get; set; }

        ///// <summary>
        ///// Метод. Добавть новую строку
        ///// </summary>
        ///// <param name="p"></param>
        //private void OnAddNewRowCommandExecutde(object p)
        //{
        //    if (SelectedSubdivision != null)
        //    {
        //        roomDtos.Add(new RoomDto()
        //        {
        //            SubdivisionId = SelectedSubdivision.Id
        //        });
        //        Rooms = CollectionViewSource.GetDefaultView(roomDtos);
        //        Rooms.Refresh();

        //        //RoomsNames = CollectionViewSource.GetDefaultView(roomsNamesList);
        //        //RoomsNames.Refresh();
        //    }
        //}
        //private bool CanAddNewRowCommandExecute(object p)
        //{
        //    if (SelectedSubdivision == null) return false;
        //    else return true;
        //}
        #endregion

        #region Копировать подразделение из другого здания

        private void OnCopySubdivisios(object obj)
        {
            MessageBox.Show("Попытка скопирвоать подразделение!");
        }
        //public ICommand CopySubdivisionCommnd { get; set; }
        //public static List<SubdivisionDto> CopySubdivisionList { get; set; }
        ///// <summary>
        ///// Метод. Добавть новую строку
        ///// </summary>
        ///// <param name="p"></param>
        //private void OnCopySubdivisionCommndExecutde(object p)
        //{
        //    //CopySubDivisionViewModel.projContext = projContext;
        //    //CopySubDivisionViewModel.selectedBuildingId = SelectedBuilding.Id;
        //    //CopySubdivisionWindow copySubdivisionWindow = new CopySubdivisionWindow();
        //    //CopySubDivisionViewModel copySubDivisionViewModel = new CopySubDivisionViewModel();
        //    //copySubDivisionViewModel.copySubdivisionWindow = copySubdivisionWindow;
        //    //copySubdivisionWindow.DataContext = copySubDivisionViewModel;
        //    //copySubdivisionWindow.ShowDialog();

        //    //Subdivisions = projContext.GetSubdivisions(SelectedBuilding);
        //}
        //private bool CanCopySubdivisionCommndExecute(object p)
        //{
        //    //if (SelectedBuilding == null) return false;

        //    return true;
        //}
        #endregion

        #region Комманд. Закинуть обновления пространств в БД

        public void OnSaveChanges(object obj)
        {
            projContext.SaveChanges();
        }

        //public ICommand PushToDbCommand { get; set; }
        //private void OnPushToDbCommandExecutde(object p)
        //{
        //    if (roomDtos != null)
        //    {
        //        projContext.AddNewRooms(roomDtos);
        //        projContext.SaveChanges();
        //        roomDtos = projContext.GetRooms(SelectedSubdivision);
        //        Rooms = CollectionViewSource.GetDefaultView(roomDtos);
        //        Rooms.Refresh();
        //        MessageBox.Show("Данные успешно загруженны в базу данных", "Статус");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Ошибка! Нет выбранных помещений", "Статус");
        //    }

        //}
        //private bool CanPushToDbCommandExecute(object p)
        //{
        //    return true;
        //}
        #endregion

        #region Выгрузка в Эксель

        public ICommand PullFromDbCommand { get; set; }
        private void OnPullFromDbCommandExecutde(object p)
        {
            MainExcelModel.CreateXslxIssues(projContext.GetRooms(SelectedSubdivision));
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


        #region Комманд. Сохранить изменения в номерах помещений

        public ICommand PushToDbSaveChangesCommand { get; set; }

        private void OnPushToDbSaveChangesCommandExecutde(object obj)
        {
            projContext.SaveChanges();
        }
        private bool CanPushToDbSaveChangesCommandExecute(object obj)
        {
            if (AllRooms != null && AllRooms.Count != 0) return true;
            else return false;
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
