using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Mediators;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels
{
    internal class ButtonPanelViewModel : ViewModel
    {
        public ButtonPanelViewModel()
        {
            SaveChangesCommand = new RelayCommand(OnSaveChangesCommandExecuted);
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecuted);
            CopySubdivisionCommnd = new RelayCommand(OnCopySubdivisionCommndExecuted);
            UploadToExcelProgramAndSummaryCommand = new RelayCommand(OnUploadToExcelProgramAndSummaryCommandExecuted);
        }

        #region Сохранить изменения в БД
        public ICommand SaveChangesCommand { get; set; }


        public void OnSaveChangesCommandExecuted(object obj)
        {
            Mediator.NotifyColleagues("SaveChanges");
        }
        #endregion

        #region Добавит новую строку в текущее подразделение

        public ICommand AddNewRowCommand { get; set; }

        public void OnAddNewRowCommandExecuted(object obj)
        {
            Mediator.NotifyColleagues("AddNewRow");
        }
        #endregion

        #region Скопировать подразделение

        public ICommand CopySubdivisionCommnd { get; set; }

        public void OnCopySubdivisionCommndExecuted(object obj)
        {
            Mediator.NotifyColleagues("CopySubdivisios");
        }
        #endregion

        #region Выгрузить в эксель

        public ICommand UploadToExcelProgramAndSummaryCommand { get; set; }

        public void OnUploadToExcelProgramAndSummaryCommandExecuted(object obj)
        {
            Mediator.NotifyColleagues("UploadProgramAndSummaryToExcel");
        }

        #endregion
    }
}
