using Microsoft.VisualBasic.FileIO;
using RoomsAndSpacesManagerDesktop.DTO;
using RoomsAndSpacesManagerDesktop.DTO.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Models.CsvModels
{
    static class MainCsvModel
    {
        //private static List<CategoryDto> categories = new List<CategoryDto>();
        //private static List<string[]> ListOfarray = new List<string[]>();
        //private static List<string[]> ListOfNames = new List<string[]>();
        //static MainCsvModel()
        //{
        //    using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Категории.csv"))
        //    {
        //        parser.TextFieldType = FieldType.Delimited;
        //        parser.SetDelimiters(",");
        //        while (!parser.EndOfData)
        //        {

        //            ListOfarray.Add(parser.ReadFields());

        //            ////Process row
        //            //string[] fields = parser.ReadFields();
                    
        //        }
        //    }

        //    using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Помещения.csv"))
        //    {
        //        parser.TextFieldType = FieldType.Delimited;
        //        parser.SetDelimiters(",");
        //        while (!parser.EndOfData)
        //        {
        //            //Process row
        //            string[] fields = parser.ReadFields();
        //            ListOfNames.Add(fields);
        //        }
        //    }




        //    foreach (var item in ListOfarray)
        //    {
        //        CategoryDto categoryDto = new CategoryDto()
        //        {
        //            Name = item[0],
        //            Key = item[1]
        //        };

        //        if (categories.Where(x => x.Name == categoryDto.Name).Count() == 0)
        //            categories.Add(categoryDto);
        //    }

        //    foreach (CategoryDto cats in categories)
        //    {
        //        foreach (string[] item in ListOfarray)
        //        {
        //            if (item[3].Contains(cats.Key))
        //            {
        //                cats.subCategoryDtos.Add(new SubCategoryDto()
        //                {
        //                    Name = item[2],
        //                    Key = item[3]
        //                });
        //            }
        //        }
                
        //    }

        //    foreach (CategoryDto cats in categories)
        //    {
        //        foreach (SubCategoryDto subCats in cats.subCategoryDtos)
        //        {
        //            foreach (var item in ListOfNames)
        //            {
        //                if (item[0] == subCats.Key)
        //                    subCats.roomNameDtos.Add(new RoomNameDto() 
        //                    {
        //                        Key = item[0],
        //                        Name = item[1]
        //                    });
        //            }
        //        }
        //    }
        //}
        //public static List<string> GetCategoties()
        //{
        //    List<string> categoryDtos = new List<string>();
        //    using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Категории.csv"))
        //    {
        //        parser.TextFieldType = FieldType.Delimited;
        //        parser.SetDelimiters(",");
        //        while (!parser.EndOfData)
        //        {
        //            ////Process row
        //            string[] fields = parser.ReadFields();
        //            //CategoryDto categoryDto = new CategoryDto()
        //            //{
        //            //    Name = fields[0],
        //            //    Key = fields[1]
        //            //};
        //            categoryDtos.Add(fields[0]);
        //        }
        //    }

        //    //List<CategoryDto> uniq = categoryDtos.Distinct().ToList();

        //    return new HashSet<string>(categoryDtos).ToList();
        //}


        //public static List<string> GetSubCategoties(string categoryDto)
        //{
        //    List<string> vs = new List<string>();
        //    if (categoryDto != null)
        //    {
        //        foreach (var item in categories.First(x => x.Name == categoryDto).subCategoryDtos)
        //        {
        //            vs.Add(item.Name);
        //        }
        //    }
            

        //    return vs;
        //}

        //public static List<string> GetRoomNames(string categoryDto, string subCategoryDto)
        //{
        //    List<string> vs = new List<string>();
        //    if (categoryDto != null && subCategoryDto != null)
        //    {
        //        foreach (var item in categories.First(x => x.Name == categoryDto).subCategoryDtos.First(x => x.Name == subCategoryDto).roomNameDtos)
        //        {
        //            vs.Add(item.Name);
        //        }
        //    }
        //    return vs;
        //}




    }
}
