using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows;
using RoomsAndSpacesManagerDesktop.DTO.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Models.CsvModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;

namespace RoomsAndSpacesManagerDesktop.DTO
{
    [Table("RaSM_Rooms")]
    public class RoomDto : ViewModel
    {
        public RoomDto()
        {
            CategoryList = MainCsvModel.GetCategoties();
        }



        #region Поля для выгрузки
        public int Id { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string RoomNumber { get; set; }
        public double Area { get; set; }
        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }
        #endregion



        #region Вспомогательные поля для интерфейса
        [NotMapped]
        public List<CategoryDto> CategoryList { get; set; }


        private CategoryDto selectedCategory;
        [NotMapped]
        
        public CategoryDto SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                Category = SelectedCategory.Name;
                GetSubCats();
            }
        }

        [NotMapped]
        public List<SubCategoryDto> SubCategoryList { get; set; }


        private SubCategoryDto selectedSubCategory;
        

        [NotMapped]
        public SubCategoryDto SelectedSubCategory
        {
            get => selectedSubCategory;
            set
            {
                selectedSubCategory = value;
                SubCategory = SelectedSubCategory.Name;
                GetRoomsNsmes();
            }
        }

        [NotMapped]
        public List<RoomNameDto> RoomNamesList { get; set; }


        private RoomNameDto selectedRoomName;
        [NotMapped]
        public RoomNameDto SelectedRoomName
        { 
            get => selectedRoomName;
            set 
            { 
                selectedRoomName = value;
                Name = SelectedRoomName.Name;
            }
        }

        #endregion








        #region Методы получения данных по подкатегориям и именам. Потом сделать через SQL
        private void GetSubCats()
        {
            SubCategoryList = MainCsvModel.GetSubCategoties(SelectedCategory);
            OnPropertyChanged(nameof(SubCategoryList));
        }

        private void GetRoomsNsmes()
        {
            RoomNamesList = MainCsvModel.GetRoomNames(SelectedSubCategory);
            OnPropertyChanged(nameof(RoomNamesList));
        }
        #endregion
    }
}
