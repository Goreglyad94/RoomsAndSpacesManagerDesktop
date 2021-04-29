using Microsoft.VisualBasic.FileIO;
using RoomsAndSpacesManagerDesktop.DTO.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Models.CsvModels
{
    class MainCsvModel
    {

        public static List<CategoryDto> GetCategoties()
        {
            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Категории.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    CategoryDto categoryDto = new CategoryDto()
                    {
                        Name = fields[0],
                        Key = fields[1]
                    };

                    if (categoryDtos.Where(x => x.Name == categoryDto.Name).Count() == 0)
                        categoryDtos.Add(categoryDto);
                }

            }

            List<CategoryDto> uniq = categoryDtos.Distinct().ToList();

            return uniq;
        }


        public static List<SubCategoryDto> GetSubCategoties(CategoryDto categoryDto)
        {
            List<SubCategoryDto> subCategoryDtos = new List<SubCategoryDto>();

            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Категории.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    SubCategoryDto subCategoryDto = new SubCategoryDto()
                    {
                        Name = fields[2],
                        Key = fields[3]
                    };
                    subCategoryDtos.Add(subCategoryDto);


                }
               
            }
            return subCategoryDtos.Where(x => x.Key.Contains(categoryDto.Key)).ToList();
        }

        public static List<RoomNameDto> GetRoomNames(SubCategoryDto subCategoryDto)
        {
            List<RoomNameDto> roomNamesDto = new List<RoomNameDto>();

            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Помещения.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    RoomNameDto roomNameDto = new RoomNameDto()
                    {
                        Name = fields[1],
                        Key = fields[0]
                    };
                    roomNamesDto.Add(roomNameDto);


                }
            }

            return roomNamesDto.Where(x => x.Key == subCategoryDto?.Key).ToList();
        }

    }
}
