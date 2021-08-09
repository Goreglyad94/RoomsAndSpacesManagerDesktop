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
        private readonly List<string> allUssuesColumns = new List<string>()
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

        const string AllIssuesRequest = "SELECT room.Id " +
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


        private readonly List<string> roomProgramColumns = new List<string>()
        {
             "№/№",
             "Наименование помещения",
             "Площадь, м^2",
             "Примечание",
        };

        const string RoomProgramRequest = "SELECT room.Id" +
            ",build.Name " +
            ",subdiv.Name " +
            ",room.ShortName " +
            ",room.Min_area " +
            ",room.RoomNumber " +
            "FROM RaSM_Rooms room " +
            "left join RaSM_SubdivisionDto subdiv on room.SubdivisionId = subdiv.Id " +
            "left join RaSM_Buildings build on subdiv.BuildingId = build.Id " +
            "left join RaSM_Projects proj on build.ProjectId = proj.Id " +
            "left join RaSM_RoomNames roomName on room.RoomNameId = roomName.Id " +
            "where proj.Id = ";


        ISqlRequestService sqlRequestService;
        IExcelService excelService;
        public UploadMainModel()
        {
            sqlRequestService = new SqlRequestModel();
            excelService = new MainExcelModel();
        }

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

        public bool UploadRoomProgram(int projectId, string projectName)
        {
            var ojectTeble = sqlRequestService.GetSqlResponse(RoomProgramRequest + projectId.ToString());

            var excelDocument = excelService.CreateExcelDocument();
            var workSheet = excelService.CraeteExcelWorksheet(excelDocument, "Программа помещений");

            excelService.WriteRow(allUssuesColumns, workSheet);

            for (int i = 0; i < ojectTeble.Count; i++)
            {
                excelService.WriteRow(ojectTeble[i], workSheet);
            }

            excelService.SaveExcelSocument(excelDocument, "Программа: " + projectName);

            return true;
        }
    }
}
