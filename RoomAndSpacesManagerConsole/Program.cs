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
            //using (SpreadsheetDocument doc = SpreadsheetDocument.Open(@"C:\Users\ya.goreglyad\Desktop\Помещения.xlsx", false))
            //{
            //    WorkbookPart bkPart = doc.WorkbookPart;
            //    DocumentFormat.OpenXml.Spreadsheet.Workbook workbook = bkPart.Workbook;
            //    DocumentFormat.OpenXml.Spreadsheet.Sheet s = workbook.Descendants<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Where(sht => sht.Name == "Категории").FirstOrDefault();
            //    WorksheetPart wsPart = (WorksheetPart)bkPart.GetPartById(s.Id);
            //    DocumentFormat.OpenXml.Spreadsheet.SheetData sheetdata = wsPart.Worksheet.Elements<DocumentFormat.OpenXml.Spreadsheet.SheetData>().FirstOrDefault();
            //    List<CategoryDto> categoryList = new List<CategoryDto>();

            //    foreach (Row r in sheetdata.Elements<Row>())
            //    {
            //        Cell[] arrayRow = r.Elements<Cell>().ToArray();


            //        CategoryDto category = new CategoryDto()
            //        {
            //            Key = arrayRow[1].CellValue?.Text,
            //            Name = arrayRow[0].CellValue?.Text

            //        };
            //        categoryList.Add(category);
            //    }
            //}

            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения - Категории.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    string text = "";
                    foreach (string field in fields)
                    {

                        text += field + " ";
                        
                    }
                    Console.WriteLine(text);
                    
                }
                Console.ReadLine();
            }
        }
    }
}
