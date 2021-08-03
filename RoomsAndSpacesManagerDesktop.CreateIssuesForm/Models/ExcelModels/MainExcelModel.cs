using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.ExcelModels
{
    class MainExcelModel
    {
        //IUploadService UploadService;
        //public MainExcelModel(IUploadService uploadService)
        //{
        //    UploadService = uploadService;
        //    bool b = uploadService.UploadToExcel();
        //}



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

        public static void CreateXslxIssues(List<RoomDto> rooms)
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
            int rowCount = 1;
            int colCount = 1;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Subdivision);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Min_area);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_personala);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_posetitelei);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Categoty_pizharoopasnosti);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_AR);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_calc);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_min);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_max);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Pritok);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Vityazhka);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Ot_vlazhnost);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_OV);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Equipment_VK);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.KEO_est_osv);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.KEO_sovm_osv);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Osveshennost_pro_obshem_osvech);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.El_Nagruzka);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Group_el_bez);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_EOM);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_SS);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Nagruzki_na_perekririe);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_AK_ATH);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_GSV);
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_HS);
            colCount = 1;
            rowCount++;

            foreach (var item in rooms)
            {
                worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Subdivision?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Min_area?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_personala?.ToString();
                colCount++;


                worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_posetitelei?.ToString();
                colCount++;


                worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                colCount++;


                worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                colCount++;




                worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                colCount++;


                worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Discription_AR?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.T_calc?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.T_min?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.T_max?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Pritok?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Vityazhka?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Ot_vlazhnost?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Discription_OV?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Equipment_VK?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.KEO_est_osv?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.KEO_sovm_osv?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Osveshennost_pro_obshem_osvech?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.El_Nagruzka?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Group_el_bez?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Discription_EOM?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Discription_SS?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Nagruzki_na_perekririe?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Discription_AK_ATH?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Discription_GSV?.ToString();
                colCount++;

                worksheet.Cells[rowCount, colCount].Value = item.Discription_HS?.ToString();
                colCount++;

                colCount = 1;




                rowCount++;
            }

            FileInfo excelFile = new FileInfo(path);
            excel.SaveAs(excelFile);
        }
    }
}
