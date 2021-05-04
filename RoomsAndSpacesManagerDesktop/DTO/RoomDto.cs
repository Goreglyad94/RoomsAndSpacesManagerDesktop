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
        static List<CategoryDto> CatsList { get; set; }
        public RoomDto()
        {
            CategoryList = MainCsvModel.GetCategoties();

        }



        #region Поля для выгрузки
        public int Id { get; set; }

        private string category;
        public string Category
        {
            get => category;
            set
            {
                Set(ref category, value);
                SubCategoryList = MainCsvModel.GetSubCategoties(Category);
            }
        }


        private string subCategory;

        public string SubCategory 
        { 
            get => subCategory;
            set 
            { 
                subCategory = value;
                RoomNamesList = MainCsvModel.GetRoomNames(Category, SubCategory);
            }
        }



        public string Name { get; set; }
        public string ShortName { get; set; }
        public string RoomNumber { get; set; }


        

        public string Equipment { get; set; }
        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }
        #endregion



        #region Вспомогательные поля для интерфейса
        [NotMapped]
        public List<string> CategoryList { get; set; }

        private List<string> subCategoryList;


        [NotMapped]
        public List<string> SubCategoryList
        {
            get => subCategoryList;
            set => Set(ref subCategoryList, value);
        }


        private List<string> roomNamesList;
        
        [NotMapped]
        public List<string> RoomNamesList
        {
            get => roomNamesList;
            set => Set(ref roomNamesList, value);
        }
        #endregion








        #region Методы получения данных по подкатегориям и именам. Потом сделать через SQL
        private void GetSubCats()
        {
            //SubCategoryList = MainCsvModel.GetSubCategoties(SelectedCategory);
            OnPropertyChanged(nameof(SubCategoryList));
        }

        private void GetRoomsNsmes()
        {
            //RoomNamesList = MainCsvModel.GetRoomNames(SelectedSubCategory);
            OnPropertyChanged(nameof(RoomNamesList));
        }
        #endregion
    }
}
