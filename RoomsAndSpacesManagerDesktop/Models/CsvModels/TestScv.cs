using Microsoft.VisualBasic.FileIO;
using RoomsAndSpacesManagerDesktop.DTO.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Models.CsvModels
{
    class TestScv
    {
        RoomsDbContext context = new RoomsDbContext();
        
        List<CategoryDto> categoryDtos;
        List<SubCategoryDto> subCategoryDtos;
        public TestScv()
        {
            ////categoryDtos = context.GetCategotyes();
            //subCategoryDtos = context.GetSubCategotyes();
            //GetRoomsCsv();
        }


        public void GetCategotiesScv()
        {
            List<CategoryDto> CatList = new List<CategoryDto>();
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Категории.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (CatList.FirstOrDefault(x => x.Name == fields[0]) == null)
                    {
                        CatList.Add(new CategoryDto()
                        {
                            Key = fields[1],
                            Name = fields[0]
                        });
                    }
                }
                context.AddCategoties(CatList);
            }
        }

        public void GetSubCatsCsv()
        {
            List<SubCategoryDto> SubCatList = new List<SubCategoryDto>();
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Категории.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    SubCatList.Add(new SubCategoryDto() 
                    {
                        Key = fields[3],
                        Name = fields[2]
                    });
                }

            }
            foreach (SubCategoryDto subCat in SubCatList)
            {
                subCat.CategotyId = categoryDtos.FirstOrDefault(x => subCat.Key.StartsWith(x.Key)).Id;
            }

            context.AddSubCategoties(SubCatList);
        }


        public void GetRoomsCsv()
        {
            List<RoomNameDto> RoomsList = new List<RoomNameDto>();
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Помещения.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    RoomsList.Add(new RoomNameDto() 
                    {
                        Key = fields[0],
                        Name = fields[1],
                        Min_area = fields[2],
                        Class_chistoti_SanPin = fields[3],
                        Class_chistoti_SP_158 = fields[4],
                        Class_chistoti_GMP = fields[5],
                        T_calc = fields[6],
                        T_min = fields[7],
                        T_max = fields[8],
                        Pritok = fields[9],
                        Vityazhka = fields[10],
                        Ot_vlazhnost = fields[11],
                        KEO_est_osv = fields[12],
                        KEO_sovm_osv = fields[13],
                        Discription_OV = fields[14],
                        Osveshennost_pro_obshem_osvech = fields[15],
                        Group_el_bez = fields[16],
                        Discription_EOM = fields[17],
                        Discription_AR = fields[18],
                        Equipment_VK = fields[19],
                        Discription_SS = fields[20],
                        Discription_AK_ATH = fields[21],
                        Discription_GSV = fields[22],
                        Categoty_Chistoti_po_san_epid = fields[23],
                        Discription_HS =  fields[24]
                    });

                }

                foreach (RoomNameDto room in RoomsList)
                {
                    string roomKey = room.Key;


                    var sdd = subCategoryDtos.FirstOrDefault(x => x.Key == roomKey);

                    if (sdd != null)
                    {
                        room.SubCategotyId = sdd.Id;
                    }
                }

                context.AddRooms(RoomsList);

            }
        }
    }
}

