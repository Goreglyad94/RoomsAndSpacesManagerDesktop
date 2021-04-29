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
            foreach (var item in context.RaSM_Projects.ToList())
            {
                context.RaSM_Models.Add(new BuildingDto() 
                {
                    ProjectId = item.Id,
                    Name = "Здание " + item.Id.ToString() + ".1"
                });
                context.RaSM_Models.Add(new BuildingDto()
                {
                    ProjectId = item.Id,
                    Name = "Здание " + item.Id.ToString() + ".2"
                });
            }
            context.SaveChanges();
        }

        public List<ProjectDto> GetProjects()
        {
            return context.RaSM_Projects.ToList();
        }

        public List<BuildingDto> GetModels(ProjectDto projDto)
        {
            return context.RaSM_Models.Where(x => x.ProjectId == projDto.Id).ToList();
        }
    }
}
