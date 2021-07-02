using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesManagerConsole.DbModel
{
    class AddToRoomNameSubCategoryIdModel
    {
        public void AddToRoomNameSubCategoryIdModelMain()
        {
            RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();

            ExcelPackage excel = new ExcelPackage(new FileInfo(@"C:\Users\ya.goreglyad\Desktop\ПомещенияВДб.xlsx"));
            var workbook = excel.Workbook;
            var worksheet = workbook.Worksheets.First();

            int rowCount = 1;
            int count = 1;
            List<string> roomNamesIsNotExcist = new List<string>();
            while (rowCount < 112)
            {
                int subCatId = Convert.ToInt32(worksheet.Cells[rowCount, 1].Value);
                string roomName = worksheet.Cells[rowCount, 3].Value.ToString();
                if (roomAndSpacesDbContext.RaSM_RoomNames.FirstOrDefault(x => x.Name == roomName) != null)
                {
                    roomAndSpacesDbContext.RaSM_RoomNames.FirstOrDefault(x => x.Name == roomName).SubCategotyId = subCatId;
                }
                else
                {

                }
                rowCount++;
            }
            roomAndSpacesDbContext.SaveChanges();

        }


        public void Change_1_Field()
        {
            RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();
            //var dfdf = roomAndSpacesDbContext.RaSM_RoomNames.Where(x => x.SubCategotyId == 0).ToList();
            roomAndSpacesDbContext.RaSM_RoomNames.RemoveRange(roomAndSpacesDbContext.RaSM_RoomNames.Where(x => x.SubCategotyId == 0));
            roomAndSpacesDbContext.SaveChanges();

        }
    }
}
