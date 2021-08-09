using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.ExcelModels;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.SqlRequestModels;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Views
{
    /// <summary>
    /// Логика взаимодействия для MainUserControl.xaml
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        

        public MainUserControl()
        {
            InitializeComponent();

            MainExcelModel mainExcelModel = new MainExcelModel();
            SqlRequestModel sqlRequestModel = new SqlRequestModel();

            DataContext = new MainViewModel();
            HeadProjects.DataContext = new SelectionProjectsViewModel();
            HeadCategories.DataContext = new SelectionCategoriesViewModel();
            BodyCreateUssue.DataContext = new CreateIssueViewModel(sqlRequestModel);
            LeftButtonPanel.DataContext = new ButtonPanelViewModel();
            BodyRoomsProgram.DataContext = new RoomsProgramViewModel();
            BodySummary.DataContext = new SummaryViewModel();
        }
    }
}
