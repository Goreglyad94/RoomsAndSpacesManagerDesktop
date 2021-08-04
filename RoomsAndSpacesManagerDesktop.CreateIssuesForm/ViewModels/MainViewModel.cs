using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Mediators;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels
{
    class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            DivisionsList = new List<string>()
            { 
                "Все разделы",
                "АР",
                "КР",
                "ВК",
                "ОВ",
                "МГТГ",
                "ХС",
                "ЭОМ",
                "СС"  
            };

            SelectedDivision = DivisionsList.FirstOrDefault();

        }
        public List<string> DivisionsList { get; set; }

        private string selectedDivision;
        public string SelectedDivision
        {
            get { return selectedDivision; }
            set 
            { 
                Set(ref selectedDivision, value);
                Mediator.NotifyColleagues("SelectDivision", SelectedDivision);
            }
        }
    }
}
