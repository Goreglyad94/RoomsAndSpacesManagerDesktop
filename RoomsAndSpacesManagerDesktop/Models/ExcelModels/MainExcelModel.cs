using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsAndSpacesManagerDesktop.Models.ExcelModels
{
    class MainExcelModel
    {
        public void AddToDbFromExcelEqupment(RoomNameDto roomName)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            MessageBox.Show(openFileDialog.FileName);



            ExcelPackage excel = new ExcelPackage(new FileInfo(openFileDialog.FileName));

            List<RoomEquipmentDto> equipnets = new List<RoomEquipmentDto>();

            string idPom = "start";

            var workbook = excel.Workbook;
            var worksheet = workbook.Worksheets.First();

            int rowCount = 2;
            int colCount = 1;

            while (idPom != "")
            {
                if (worksheet.Cells[rowCount, colCount].Value == null)
                    break;

                RoomEquipmentDto roomEquipmentDto = new RoomEquipmentDto();

                roomEquipmentDto.RoomNameId = roomName.Id;

                if (worksheet.Cells[rowCount, 2].Value != null)
                    roomEquipmentDto. Number = Convert.ToInt32(worksheet.Cells[rowCount, 2].Value);

                if (worksheet.Cells[rowCount, 3].Value != null)
                    roomEquipmentDto.ClassificationCode = worksheet.Cells[rowCount, 3].Value.ToString();



                if (worksheet.Cells[rowCount, 4].Value != null)
                    roomEquipmentDto.TypeName = worksheet.Cells[rowCount, 4].Value.ToString();

                if (worksheet.Cells[rowCount, 5].Value != null)
                    roomEquipmentDto.Name = worksheet.Cells[rowCount, 5].Value.ToString();

                if (worksheet.Cells[rowCount, 6].Value != null)
                    roomEquipmentDto.Count = Convert.ToInt32(worksheet.Cells[rowCount, 6].Value);



                string ddd = worksheet.Cells[rowCount, 7].Value.ToString();

                if (worksheet.Cells[rowCount, 7].Value != null)
                {
                    if (ddd == "True")
                        roomEquipmentDto.Mandatory = true;
                }



                equipnets.Add(roomEquipmentDto);

                rowCount++;
            }

            EquipmentDbContext context = new EquipmentDbContext();
            context.AddNewEquipments(equipnets);


        }
    }
}
