using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RoomsAndSpacesManagerDesktop.DTO;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;


namespace RoomsAndSpacesManagerDesktop.Models.DbModels
{
    public class ProjectsDbContext : MainDbContext
    {
        public void DB()
        {
            
        }

        public List<ProjectDto> GetProjects()
        {
            return context.RaSM_Projects.ToList();
        }

        public List<BuildingDto> GetModels(ProjectDto projDto)
        {
            return context.RaSM_Models.Where(x => x.ProjectId == projDto.Id).ToList();
        }



        public void AddNewRooms(BuildingDto buildDto, List<RoomDto> rooms)
        {
            context.RaSM_Rooms.AddRange(rooms);
            context.SaveChanges();
        }

        public List<RoomDto> GetRooms(BuildingDto buildDto)
        {
            if (buildDto != null)
                return context.RaSM_Rooms.Where(x => x.Building.Id == buildDto.Id).ToList();
            else
                return null;
        }
    }
}
