using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.ExcelModels
{
    class MainExcelModel : IExcelService
    {   
        public ExcelPackage CreateExcelDocument()
        {
            return new ExcelPackage();
        }

        public ExcelWorksheet CraeteExcelWorksheet(ExcelPackage excelPackage, string worksheetName)
        {
            if (excelPackage.Workbook.Worksheets[worksheetName] == null)
                excelPackage.Workbook.Worksheets.Add(worksheetName);

            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[worksheetName];

            curentWorksheetName = worksheetName;

            return worksheet;
        }
        private static int rowCount = 1;
        private static string curentWorksheetName = string.Empty;
        public bool WriteRow(List<string> row, ExcelWorksheet worksheet)
        {
            if (worksheet.Name != curentWorksheetName)
            {
                rowCount = 1;
            }

            for (int i = 0; i < row.Count; i++)
            {
                worksheet.Cells[rowCount, i + 1].Value = row[i];
            }

            rowCount++;

            return true;
        }

        public bool SaveExcelSocument(ExcelPackage excelPackage, string fileName)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();
            string path = openFileDialog.SelectedPath + "\\" + fileName + ".xlsx";

            FileInfo excelFile = new FileInfo(path);
            excelPackage.SaveAs(excelFile);

            rowCount = 1;

            return true;
        }
    }
}
