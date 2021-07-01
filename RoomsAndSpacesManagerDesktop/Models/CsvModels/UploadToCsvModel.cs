using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsAndSpacesManagerDesktop.Models.CsvModels
{
    internal class UploadToCsvModel
    {
        RoomAndSpacesDbContext context = new RoomAndSpacesDbContext();
        internal void UploadRoomProgram(ProjectDto project)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();

            openFileDialog.ShowDialog();

            var dfdd = openFileDialog.SelectedPath;

            var isExist = File.Exists(dfdd + @"\Программа.csv");

            if (isExist)
                File.Delete(dfdd + @"\Программа.csv");

            using (var dd = File.Create(dfdd + @"\Программа.csv"))
            {
                StreamWriter sw = new StreamWriter(dd, Encoding.GetEncoding("Windows-1251"));
                sw.WriteLine("№/№" + ";" + "Наименование помещения" + ";" + "Площадь, м^2" + ";" + "Примечание");
                int i = 1;
                foreach (BuildingDto build in context.RaSM_Projects.FirstOrDefault(x => x.Id == project.Id).Buildings)
                {
                    sw.WriteLine('\'' + i.ToString() + ";" + build.Name + ";" + ";");
                    int ii = 1;
                    foreach (SubdivisionDto subdivision in build.Subdivisions)
                    {
                        sw.WriteLine('\'' + i.ToString() + "." + ii.ToString() + ";" + subdivision.Name + ";" + ";");
                        int iii = 1;
                        foreach (RoomDto room in subdivision.Rooms)
                        {
                            sw.WriteLine('\'' + i.ToString() + "." + ii.ToString() + "." + iii.ToString() + ";" + room.Name + ";" + room.Min_area + ";" + room.Notation);
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }
                sw.Close();
            }
        }

        internal void UploadRoomProgramToExcel(ProjectDto project)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();

            openFileDialog.ShowDialog();

            var dfdd = openFileDialog.SelectedPath;


            var isExist = File.Exists(dfdd + @"\Программа.xlsx");

            if (isExist)
                File.Delete(dfdd + @"\Программа.xlsx");

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Программа помещений");

            var worksheet = excel.Workbook.Worksheets["Программа помещений"];


            int rowCount = 1;
            int colCount = 1;


            //"№/№" + ";" + "Наименование помещения" + ";" + "Площадь, м^2" + ";" + "Примечание")


            worksheet.Cells[rowCount, colCount].Value = "№/№";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Площадь, м^2";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Примечание";
            colCount = 1;
            rowCount++;


            int i = 1;
            foreach (BuildingDto build in context.RaSM_Projects.FirstOrDefault(x => x.Id == project.Id).Buildings)
            {
                worksheet.Cells[rowCount, colCount].Value = i;
                colCount++;
                worksheet.Cells[rowCount, colCount].Value = build.Name;
                colCount = 1;
                rowCount++;
                int ii = 1;
                foreach (SubdivisionDto subdivision in build.Subdivisions)
                {
                    string iis = i.ToString() + "." + ii.ToString();

                    worksheet.Cells[rowCount, colCount].Value = iis;
                    colCount++;
                    worksheet.Cells[rowCount, colCount].Value = subdivision.Name;
                    colCount = 1;
                    rowCount++;

                    int iii = 1;
                    foreach (RoomDto room in subdivision.Rooms)
                    {
                        string iiis = i.ToString() + "." + ii.ToString() + "." + iii.ToString();

                        worksheet.Cells[rowCount, colCount].Value = iiis;
                        colCount++;
                        worksheet.Cells[rowCount, colCount].Value = room.ShortName;
                        colCount++;
                        worksheet.Cells[rowCount, colCount].Value = room.Min_area;
                        colCount++;
                        worksheet.Cells[rowCount, colCount].Value = room.Notation;
                        colCount = 1;
                        rowCount++;
                        iii++;
                    }
                    ii++;
                }
                i++;
            }

            FileInfo excelFile = new FileInfo(dfdd + @"\Программа.xlsx");
            excel.SaveAs(excelFile);
        }
    }
}
