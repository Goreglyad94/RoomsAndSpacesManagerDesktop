using RoomsAndSpacesManagerDataBase;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Mediators;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels
{
    class RoomsProgramViewModel : ViewModel
    {
        public RoomsProgramViewModel()
        {
            #region Медиаторы
            Mediator.Register("ThrowProjectOnRoomProgramViewModel", OnGetProject); 
            #endregion
        }


        #region Медиатор. Получить выбранный проект.

        private void OnGetProject(object obj)
        {
            ProjectsDbContext projectsDbContext = new ProjectsDbContext();
            AllRooms = projectsDbContext.GetAllRoomsByProject(obj as ProjectDto);
        }

        #endregion

        #region Список помещений для целого проекта с сортировкой. Помещения получаются при выборе проекта. 
        private List<RoomDto> allRooms;


        public List<RoomDto> AllRooms
        {
            get { return allRooms; }
            set { Set(ref allRooms, value); }
        }
        #endregion

    }
}
