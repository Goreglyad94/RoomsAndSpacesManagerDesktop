﻿using System;
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
            CsvToDbRooms csvToDbRooms = new CsvToDbRooms();
            csvToDbRooms.AddCats();
            csvToDbRooms.GetSubCatsCsv();
            csvToDbRooms.AddRooms();
        }
    }
}
