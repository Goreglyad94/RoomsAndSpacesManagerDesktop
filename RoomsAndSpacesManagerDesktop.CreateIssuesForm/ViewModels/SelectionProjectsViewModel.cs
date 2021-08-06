using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Mediators;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels
{
    internal class SelectionProjectsViewModel : ViewModel
    {

        ProjectsDbContext projContext = new ProjectsDbContext();

        public SelectionProjectsViewModel()
        {

            Projects = projContext.GetProjects();

            #region Комманды. Регистрация
            AddNewProjectCommand = new RelayCommand(OnAddNewProjectCommandExecutde, CanAddNewProjectCommandExecute);
            AddNewBuildingCommand = new RelayCommand(OnAddNewBuildingCommandExecutde, CanAddNewBuildingCommandExecute);
            DeleteCommand = new RelayCommand(OnDeleteCommandExecutde, CanDeleteCommandExecute);
            AddNewSubdivisionCommand = new RelayCommand(OnAddNewSubdivisionCommandExecutde, CanAddNewSubdivisionCommandExecute); 
            #endregion

        }

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

                    Mediator.NotifyColleagues("ThrowProjectOnRoomProgramViewModel", SelectedProject);
                    Mediator.NotifyColleagues("ThrowProjectOnSummaryViewModel", SelectedProject);
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
                    Mediator.NotifyColleagues("ThrowSubdivision", SelectedSubdivision);

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

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    }
}
