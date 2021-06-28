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
                    sw.WriteLine(i.ToString() + ";" + build.Name + ";" + ";");
                    int ii = 1;
                    foreach (SubdivisionDto subdivision in build.Subdivisions)
                    {
                        sw.WriteLine(i.ToString() + "." + ii.ToString() + ";" + subdivision.Name + ";" + ";");
                        int iii = 1;
                        foreach (RoomDto room in subdivision.Rooms)
                        {
                            sw.WriteLine(i.ToString() + "." + ii.ToString() + "." + iii.ToString() + ";" + room.Name + ";" + room.Min_area + ";" + room.Notation);
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }
                sw.Close();
            }

        }
    }
}
