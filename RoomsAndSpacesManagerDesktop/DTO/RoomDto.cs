using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows;
using RoomsAndSpacesManagerDesktop.DTO.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;

namespace RoomsAndSpacesManagerDesktop.DTO
{
    [Table("RaSM_Rooms")]
    public class RoomDto : ViewModel
    {
        public List<string> _SubCategoryList;
        private string selectedCategory;

        public RoomDto()
        {
            _SubCategoryList = new List<string>()
            {
            "1.1",
            "1.2",
            "1.3",

            "2.1",
            "2.2",

            "3.1"
            };
        }
        public int Id { get; set; }


        public List<string> CategoryList { get; set; } = new List<string>()
        {
            "1",
            "2",
            "3"
        };
        public string SelectedCategory 
        { 
            get => selectedCategory;
            set 
            { 
                selectedCategory = value;
                GetSubCats();
            }
        }


        public List<string> SubCategoryList { get; set; }
        public string SelectedSubCategory { get; set; }


        public string Name { get; set; }
        public string ShortName { get; set; }
        public string RoomNumber { get; set; }
        public double Area { get; set; }

        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }
        private void GetSubCats()
        {
            SubCategoryList = _SubCategoryList.Where(x => x.StartsWith(SelectedCategory)).ToList();
            OnPropertyChanged(nameof(SubCategoryList));
        }
    }
}
