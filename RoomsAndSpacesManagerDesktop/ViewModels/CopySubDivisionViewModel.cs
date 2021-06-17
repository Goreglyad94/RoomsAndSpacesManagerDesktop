using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class CopySubDivisionViewModel : ViewModel
    {
        public CopySubdivisionWindow copySubdivisionWindow;
        public static ProjectsDbContext projContext;
        public static int selectedBuildingId;
        public CopySubDivisionViewModel()
        {
            Projects = projContext.GetProjects();
            CopySubdivisionCommand = new RelayCommand(OnCopySubdivisionCommandExecuted, CanCopySubdivisionCommandExecute);
        }

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
                Set(ref selectedSubdivision, value);
            }
        }
        #endregion


        #region Комманда Apply
        public ICommand CopySubdivisionCommand { get; set; }
        private void OnCopySubdivisionCommandExecuted(object obj)
        {
            List<SubdivisionDto> copyListOfSubdivision = new List<SubdivisionDto>();
            foreach (SubdivisionDto subdivision in Subdivisions)
            {
                if (subdivision.IsChecked)
                {
                    SubdivisionDto newSubdivision = new SubdivisionDto(subdivision)
                    {
                        BuildingId = selectedBuildingId
                    };
                    projContext.AddNewSubdivision(newSubdivision);


                    List<RoomDto> newRooms = new List<RoomDto>();
                    foreach (RoomDto rooms in projContext.GetRooms(subdivision))
                    {
                        newRooms.Add(new RoomDto(rooms) { 
                            SubdivisionId = newSubdivision.Id
                        });
                    }

                    projContext.AddNewRooms(newRooms);

                }
            }
            copySubdivisionWindow.Close();
        }

        private bool CanCopySubdivisionCommandExecute(object obj) => true;
        #endregion
    }
}
