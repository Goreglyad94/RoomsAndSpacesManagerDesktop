using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.ExcelModels;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.SqlRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.UploadModels
{
    class UploadMainModel : IUploadService
    {
        private readonly string AllIssuesRequest;
        private readonly List<string> allUssuesColumns;
        public UploadMainModel()
        {
            sqlRequestService = new SqlRequestModel();
            excelService = new MainExcelModel();

            AllIssuesRequest = "SELECT room.Id " +
                                        ", build.Name " +
                                        ",subdiv.Name " +
                                        ",roomName.Name " +
                                        ",room.ShortName " +
                                        ",room.RoomNumber " +
                                        ",room.Min_area " +
                                        ",room.Kolichestvo_personala " +
                                        ",room.Kolichestvo_posetitelei " +
                                        ",room.Categoty_pizharoopasnosti " +
                                        ",room.Class_chistoti_SanPin " +
                                        ",room.Class_chistoti_SP_158 " +
                                        ",room.Class_chistoti_GMP " +
                                        ",room.Discription_AR " +
                                        ",room.T_calc " +
                                        ",room.T_min " +
                                        ",room.T_max " +
                                        ",room.Pritok " +
                                        ",room.Vityazhka " +
                                        ",room.Ot_vlazhnost " +
                                        ",room.Discription_OV " +
                                        ",room.Equipment_VK " +
                                        ",room.KEO_est_osv " +
                                        ",room.KEO_sovm_osv " +
                                        ",room.Osveshennost_pro_obshem_osvech " +
                                        ",room.El_Nagruzka " +
                                        ",room.Group_el_bez " +
                                        ",room.Discription_EOM " +
                                        ",room.Discription_SS " +
                                        ",room.Nagruzki_na_perekririe " +
                                        ",room.Discription_AK_ATH " +
                                        ",room.Discription_GSV " +
                                        ",room.Discription_HS " +
                                        "FROM RaSM_Rooms room " +
                                        "left join RaSM_SubdivisionDto subdiv on room.SubdivisionId = subdiv.Id " +
                                        "left join RaSM_Buildings build on subdiv.BuildingId = build.Id " +
                                        "left join RaSM_Projects proj on build.ProjectId = proj.Id " +
                                        "left join RaSM_RoomNames roomName on room.RoomNameId = roomName.Id " +
                                        "where proj.Id = ";

            allUssuesColumns = new List<string>()
        {
             "Id",
             "Здание",
             "Подразделение",
             "Имя помещения",
             "Имя помещения",
             "Номер помещения",
             "Минимальная площадь",
             "Количество персонала",
             "Количество посетителей",
             "Категория пожароопасности",
             "Класс чистоты по СанПИН",
             "Класс чистоты по СП 158",
             "Класс чистоты GMP",
             "Примечание АР",
             "Расчетная температура",
             "Минимальная температура",
             "Максимальная температура",
             "Приток",
             "Вытяжка",
             "Относительная влажность",
             "Примечание ОВ",
             "Примечание ВК",
             "КЕО естественного освещения",
             "КЕО совмещенного освещения",
             "Освещенность при общем освещении",
             "Электрическая нагрузка",
             "Группа по электробезопасности",
             "Примечание ЭОМ",
             "Примечание СС",
             "Нагрузка на перекрытие",
             "Примечание АК/АТХ",
             "Примечание ГСВ",
             "Примечание ХС"
        };

        }

        ISqlRequestService sqlRequestService;
        IExcelService excelService;


        RoomAndSpacesDbContext context = new RoomAndSpacesDbContext();

        public bool UploadAllUssues(int projectId, string projectName)
        {
            var ojectTeble = sqlRequestService.GetSqlResponse(AllIssuesRequest + projectId.ToString());

            var excelDocument = excelService.CreateExcelDocument();
            var workSheet = excelService.CraeteExcelWorksheet(excelDocument, "Задания на помещения по проекту");

            excelService.WriteRow(allUssuesColumns, workSheet);

            for (int i = 0; i < ojectTeble.Count; i++)
            {
                excelService.WriteRow(ojectTeble[i], workSheet);
            }

            excelService.SaveExcelSocument(excelDocument, projectName);
            return true;
        }

        public bool UploadRoomProgram(ProjectDto project, double k)
        {
            //var ojectTeble = sqlRequestService.GetSqlResponse(RoomProgramRequest + projectId.ToString());

            var excelDocument = excelService.CreateExcelDocument();
            ExcelWorksheet workSheetRoomProgram = excelService.CraeteExcelWorksheet(excelDocument, "Программа помещений");
            UploadRoomProgramToExcel(project, ref workSheetRoomProgram);

            ExcelWorksheet workSheetSummary = excelService.CraeteExcelWorksheet(excelDocument, "Сводная");
            //UploadRoomSummaryToExcel(project, ref workSheetSummary, k);

            excelService.SaveExcelSocument(excelDocument, "Программа - " + project.Name);

            return true;
        }

        private void UploadRoomProgramToExcel(ProjectDto project, ref ExcelWorksheet worksheet)
        {
            int rowCount = 1;
            int colCount = 1;


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
        }



        public bool UploadRoomSummary(List<BuildingDto> buildings, double Koef, ref ExcelWorksheet worksheet)
        {
            int rowCount = 1;
            int colCount = 1;

            worksheet.Cells[rowCount, colCount].Value = "№/№";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Подразделение";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Площадь расчётная, м^2";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Ориент. общая площадь, м^2";
            colCount = 1;
            rowCount++;

            int n1 = 1;

            double sumarea = 0;
            double Ksumarea = 0;
            foreach (BuildingDto build in buildings)
            {
                worksheet.Cells[rowCount, 1].Value = n1.ToString();
                worksheet.Cells[rowCount, 2].Value = build.Name;
                worksheet.Cells[rowCount, 3].Value = build.SunnuryArea;
                sumarea += Convert.ToDouble(build.SunnuryArea);
                Ksumarea += Convert.ToDouble(build.SunnuryArea) * Koef;
                worksheet.Cells[rowCount, 4].Value = build.SunnuryArea * Koef;
                rowCount++;

                int n2 = 1;
                foreach (SubdivisionDto subdiv in build.Subdivisions)
                {
                    worksheet.Cells[rowCount, 1].Value = n1.ToString() + "." + n2.ToString();
                    worksheet.Cells[rowCount, 2].Value = subdiv.Name;
                    worksheet.Cells[rowCount, 3].Value = subdiv.SunnuryArea;
                    worksheet.Cells[rowCount, 4].Value = subdiv.SunnuryArea * Koef;
                    n2++;
                    rowCount++;
                }
                n1++;
            }
            worksheet.Cells[rowCount, 3].Value = sumarea;
            worksheet.Cells[rowCount, 4].Value = Ksumarea;


            return true;
        }
    }
}