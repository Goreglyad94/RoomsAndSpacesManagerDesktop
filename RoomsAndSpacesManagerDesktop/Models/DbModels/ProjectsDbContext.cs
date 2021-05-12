using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;


namespace RoomsAndSpacesManagerDesktop.Models.DbModels
{
    public class ProjectsDbContext : MainDbContext
    {
        public void DB()
        {
            
        }

        /// <summary>
        /// Добавить новый проект в БД
        /// </summary>
        /// <param name="proj"></param>
        public void AddNewProjects(ProjectDto proj)
        {
            context.RaSM_Projects.Add(proj);
            context.SaveChanges();
        }

        /// <summary>
        /// Добавить новое здание в БД
        /// </summary>
        /// <param name="proj"></param>
        public void AddNewBuilding(BuildingDto proj)
        {
            context.RaSM_Buildings.Add(proj);
            context.SaveChanges(); 
        }


        public List<ProjectDto> GetProjects()
        {
            return context.RaSM_Projects.ToList();
        }

        public void RemoveProject(ProjectDto projDto)
        {
            context.RaSM_Projects.Remove(projDto);
            context.SaveChanges();
        }

        public void RemoveBuilding(BuildingDto buildDto)
        {
            context.RaSM_Buildings.Remove(buildDto);
            context.SaveChanges();
        }


        public List<BuildingDto> GetModels(ProjectDto projDto)
        {
            return context.RaSM_Buildings.Where(x => x.ProjectId == projDto.Id).ToList();
        }



        public void AddNewRooms(BuildingDto buildDto, List<RoomDto> rooms)
        {
            context.RaSM_Rooms.AddRange(rooms.Where(x => x.Id == default).ToList());
            context.SaveChanges();
        }

        public List<RoomDto> GetRooms(BuildingDto buildDto)
        {
            if (buildDto != null)
                return context.RaSM_Rooms.Where(x => x.Building.Id == buildDto.Id).ToList();
            else
                return null;
        }

        public void RemoveRoom(RoomDto room)
        {
            context.RaSM_Rooms.Remove(room);
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
