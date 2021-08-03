using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.CustomControlClasses
{
    public class CustomComboboxSelectDivision : ComboBox
    {
        public CustomComboboxSelectDivision()
        {
            ItemsSource = new List<string>() 
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

            SelectedItem = "Все разделы";

        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            Mediator.NotifyColleagues("ThrowDivision", e.AddedItems[0]);
            base.OnSelectionChanged(e);
        }
    }
}
