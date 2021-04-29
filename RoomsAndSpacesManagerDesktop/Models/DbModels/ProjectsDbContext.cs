using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RoomsAndSpacesManagerDesktop.DTO;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;


namespace RoomsAndSpacesManagerDesktop.Models.DbModels
{
    public class ProjectsDbContext : MainDbContext
    {
        public List<ProjectDto> GetProjects()
        {
            if (context.RaSM_Projects != null)
                return context.RaSM_Projects.ToList();

            else return null;
        }

        public void AddProject(ProjectDto proj)
        {

            //if (context.RaSM_Projects.Where(x => x.Name == proj.Name).Count() == 0)
            //{
                context.RaSM_Projects.Add(proj);
                context.SaveChanges();

            //    if (context.RaSM_Projects != null)
            //    {
            //        return context.RaSM_Projects.ToList();
            //    }
            //    else return null;
            //}
            //else
            //{
            //    MessageBox.Show("Такой проект уже есть", "Статус");
            //    return context.RaSM_Projects.ToList();
            //}
        }
    }
}
