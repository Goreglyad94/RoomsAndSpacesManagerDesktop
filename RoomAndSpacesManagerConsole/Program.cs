using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic.FileIO;

namespace RoomAndSpacesManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RoomInfoToDb();
            //ProjectInfoToDb();
        }

        public static void RoomInfoToDb()
        {
            CsvToDbRooms csvToDbRooms = new CsvToDbRooms();
            csvToDbRooms.AddCats();
            csvToDbRooms.GetSubCatsCsv();
            csvToDbRooms.AddRooms();
        }

        public static void ProjectInfoToDb()
        {
            AddInfoToDb projCont = new AddInfoToDb();
            projCont.ProjectInfoToDB("Екатеринбург", "ЕКА-ЦКЛ");
        }
    }
}
