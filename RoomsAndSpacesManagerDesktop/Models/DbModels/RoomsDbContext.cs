using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Models.DbModels
{
    class RoomsDbContext : MainDbContext
    {
        public void AddCategoties(List<CategoryDto> subCategoryDtos)
        {
            context.RaSM_RoomCategories.AddRange(subCategoryDtos);
            context.SaveChanges();
        }

        public List<CategoryDto> GetCategotyes()
        {
            return context.RaSM_RoomCategories.ToList();
        }
        public void AddSubCategoties(List<SubCategoryDto> subCategoryDtos)
        {
            context.RaSM_RoomSubCategories.AddRange(subCategoryDtos);
            context.SaveChanges();
        }
        public List<SubCategoryDto> GetSubCategotyes()
        {
            return context.RaSM_RoomSubCategories.ToList();
        }
        public void AddRooms(List<RoomNameDto> roomNameDtos)
        {
            context.RaSM_RoomNames.AddRange(roomNameDtos);
            context.SaveChanges();
        }




    }
}
