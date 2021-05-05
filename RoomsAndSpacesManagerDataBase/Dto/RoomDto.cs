using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDataBase.Dto
{
    [Table("RaSM_Rooms")]
    public class RoomDto : ViewModel
    {
        static RoomAndSpacesDbContext context = new RoomAndSpacesDbContext();

        public RoomDto()
        {

        }

        #region Поля для выгрузки
        public int Id { get; set; }

        private string category;
        public string Category
        {
            get => category;
            set
            {
                category = value;
                if (Category != null)
                {
                    int catId = CategoryList.FirstOrDefault(x => x?.Name == Category).Id;
                    SubCategoryList = context.RaSM_RoomSubCategories.Where(x => x.CategotyId == catId).ToList();
                }

            }
        }

        private string subCategory;
        public string SubCategory
        {
            get => subCategory;
            set
            {
                subCategory = value;
                if (SubCategory != null)
                {
                    int catId = SubCategoryList.FirstOrDefault(x => x?.Name == SubCategory).Id;
                    RoomNamesList = context.RaSM_RoomNames.Where(x => x.SubCategotyId == catId).ToList();
                }

            }
        }


        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                if (Name != null)
                    RoomNameDto = RoomNamesList.FirstOrDefault(x => x.Name == Name);
            }
        }
        public string ShortName { get; set; }
        public string RoomNumber { get; set; }

        public int BuildingId { get; set; }

        public virtual BuildingDto Building { get; set; }
        #endregion



        #region Вспомогательные поля для интерфейса
        [NotMapped]
        public List<CategoryDto> CategoryList { get; set; } = context.RaSM_RoomCategories.ToList();



        private List<SubCategoryDto> subCategoryList;
        [NotMapped]
        public List<SubCategoryDto> SubCategoryList
        {
            get => subCategoryList;
            set => Set(ref subCategoryList, value);
        }


        private List<RoomNameDto> roomNamesList;


        [NotMapped]
        public List<RoomNameDto> RoomNamesList
        {
            get => roomNamesList;
            set => Set(ref roomNamesList, value);
        }
        [NotMapped]
        public RoomNameDto RoomNameDto { get; set; }
        #endregion
    }
}
