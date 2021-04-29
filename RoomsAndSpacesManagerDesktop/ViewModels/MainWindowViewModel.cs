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

            #endregion
        }
        /*TabControl1 - Создание нового проекта и здания~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/



        
        


        /*TabControl2 - Список проектов и таблица из бд~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
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
                Buildings = context.GetModels(SelectedProject);
            }
        }




        private List<BuildingDto> buildings;
        /// <summary>
        /// Список проектов. Из БД
        /// </summary>
        public List<BuildingDto> Buildings
        {
            get => buildings;
            set => Set(ref buildings, value);
        }



        /*TubControl2 - таблица данных~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        #region DataGrid - Rooms
        /// <summary>
        /// Привязка DataGrid к коллекции Rooms
        /// </summary>
        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();

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
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    }
}
