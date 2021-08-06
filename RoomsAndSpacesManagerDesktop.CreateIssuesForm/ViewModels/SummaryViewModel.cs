using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Mediators;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels
{
    class SummaryViewModel : ViewModel
    {
        public SummaryViewModel()
        {
            #region Медиаторы
            Mediator.Register("ThrowProjectOnSummaryViewModel", OnGetProject);
            #endregion
        }

        #region Медиатор. Выбранный проект

        private void OnGetProject(object obj)
        {
            ProjectDto SelectedProject = obj as ProjectDto;
            int? sss = 0;
            if (SelectedProject != null)
            {
                ProjectsDbContext projContext = new ProjectsDbContext();
                List<BuildingDto>  buildList = projContext.GetModels(SelectedProject);
                foreach (var build in buildList)
                {
                    int? summAreaBuild = 0;
                    foreach (var subDiv in build.Subdivisions)
                    {
                        int? summAreaSubdiv = 0;
                        foreach (var room in subDiv.Rooms)
                        {
                            if (room.Min_area != null)
                            {
                                int i;
                                int.TryParse(room.Min_area, out i);
                                summAreaSubdiv += i;
                            }

                        }
                        subDiv.SunnuryArea = summAreaSubdiv;
                        summAreaBuild += summAreaSubdiv;
                    }
                    build.SunnuryArea = summAreaBuild;
                    sss += summAreaBuild;
                }
                Summury = CollectionViewSource.GetDefaultView(buildList);
                Summury.Refresh();
                SummuryArea = sss;
            }
        }

        #endregion

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

        #region Итоговая площадь
        private int? summuryArea;
        public int? SummuryArea
        {
            get { return summuryArea; }
            set { Set(ref summuryArea, value); }
        }
        #endregion

        #region Коэффициент умножения площади

        private double koef = 2.5;

        public double Koef
        {
            get { return koef; }
            set { koef = value; }
        }

        #endregion
    }
}
