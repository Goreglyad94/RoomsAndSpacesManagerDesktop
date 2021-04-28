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
        ProjectsDbContext projectsDbContext = new ProjectsDbContext();
        #endregion

        public MainWindowViewModel()
        {
            


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
        public ObservableCollection<RoomDto> rooms { get; set; } = new ObservableCollection<RoomDto>();
        #endregion


        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    }
}
