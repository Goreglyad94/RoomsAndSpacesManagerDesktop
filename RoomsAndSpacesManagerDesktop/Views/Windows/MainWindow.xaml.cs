using RoomsAndSpacesManagerDesktop.ViewModels;
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
using System.Windows.Shapes;

namespace RoomsAndSpacesManagerDesktop.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateIssueViewModel createIssueViewModel = new CreateIssueViewModel();
            CreateIssue.DataContext = createIssueViewModel;
            ArTub.DataContext = createIssueViewModel;
            VkTab.DataContext = createIssueViewModel;
            MgtgTab.DataContext = createIssueViewModel;
            KrTab.DataContext = createIssueViewModel;
            OvTab.DataContext = createIssueViewModel;
            EomTab.DataContext = createIssueViewModel;
            SsTab.DataContext = createIssueViewModel;
            HsTab.DataContext = createIssueViewModel;
        }
    }
}
