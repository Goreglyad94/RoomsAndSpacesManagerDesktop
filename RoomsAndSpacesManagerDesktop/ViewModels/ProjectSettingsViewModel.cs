using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class ProjectSettingsViewModel : ViewModel
    {
        List<SubdivisionDto> subdivisionsList = new List<SubdivisionDto>();
        ProjectsDbContext context;
        ProjectSettingsWindow window;
        public ProjectSettingsViewModel()
        {
            
        }
        public ProjectSettingsViewModel(List<SubdivisionDto> _subdivisionsList, ref ProjectsDbContext _context, ProjectSettingsWindow _window)
        {
            ApplyChangesCommand = new RelayCommand(OnApplyChangesCommandExecuted, CanApplyChangesCommandExecute);

            subdivisionsList = _subdivisionsList;
            context = _context;
            Subdivisions = CollectionViewSource.GetDefaultView(subdivisionsList);
            Subdivisions.Refresh();
        }


        private ICollectionView subdivisions;

        public ICollectionView Subdivisions
        {
            get { return subdivisions; }
            set { Set(ref subdivisions, value); }
        }


        public ICommand ApplyChangesCommand { get; set; }

        private void OnApplyChangesCommandExecuted(object obj)
        {
            context.SaveChanges();
            window.Close();
        }

        private bool CanApplyChangesCommandExecute(object obj) => true;


    }
}