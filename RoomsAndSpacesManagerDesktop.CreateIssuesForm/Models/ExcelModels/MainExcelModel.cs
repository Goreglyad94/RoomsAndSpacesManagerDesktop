using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.ExcelModels
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
                    roomEquipmentDto.Number = Convert.ToInt32(worksheet.Cells[rowCount, 2].Value);

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

        public static void CreateXslxProgramAndSummary(List<RoomDto> rooms)
        {
            ExcelPackage excel = new ExcelPackage();

            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();
            string path;
            path = openFileDialog.SelectedPath + "\\" + rooms.First().Subdivision.Name + ".xlsx";


            if (File.Exists(path))
                File.Delete(path);

            excel.Workbook.Worksheets.Add("Сформированное задание");

            var worksheet = excel.Workbook.Worksheets["Сформированное задание"];

            AddColumnsToExcel(rooms.GetType().GetProperties().Select(x => x.Name).ToList(), worksheet);

            for (int i = 0; i < rooms.Count; i++)
            {
                var propValue = (rooms[i] as RoomDto).GetType().GetProperties().Select(x => (x as PropertyInfo).GetValue(rooms[i])?.ToString())?.ToList();
                AddRowToExcel(propValue, worksheet, i + 2);
            }

            FileInfo excelFile = new FileInfo(path);
            excel.SaveAs(excelFile);
        }

        private static void AddColumnsToExcel(List<string> columns, ExcelWorksheet worksheet)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = columns[i];
            }
        }

        private static void AddRowToExcel(List<string> row, ExcelWorksheet worksheet, int rowIndex)
        {
            for (int i = 0; i < row.Count; i++)
            {
                worksheet.Cells[rowIndex, i + 1].Value = row[i];
            }
        }
    }
}
