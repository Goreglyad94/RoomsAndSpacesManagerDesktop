using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces
{
    public interface IExcelService
    {
        ExcelPackage CreateExcelDocument();
        ExcelWorksheet CraeteExcelWorksheet(ExcelPackage excelPackage, string worksheetName);
        bool WriteRow(List<string> row, ExcelWorksheet workSheet);
        bool SaveExcelSocument(ExcelPackage excelPackage, string fileName);

    }
}
