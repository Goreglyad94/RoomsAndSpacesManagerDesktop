using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Models.ExcelModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Models.SqlModel
{
    public static class SqlMainModel
    {
        static SqlDataReader sqlDataReader;
        private static string connectionString = @"Data Source=nt-db01.ukkalita.local;Initial Catalog=M1_Revit;integrated security=True;MultipleActiveResultSets=True";
        /// <summary>
        /// Получает список оборудования (SQL) из базы данных по выбранному проекту. Возвращает список Id оборудования с конкретным проектом
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public static SqlDataReader GetEqupmentByProjects(ProjectDto project)
        {
            string sqlExpression = "select e.Id, b.Name, s.Name, r.ShortName, e.ClassificationCode, e.Name, e.Count, e.Number, e.Mandatory from RaSM_Equipments e INNER JOIN RaSM_Rooms r ON e.RoomId = r.Id INNER JOIN RaSM_SubdivisionDto s ON r.SubdivisionId = s.Id inner join RaSM_Buildings b on s.BuildingId = b.Id inner join RaSM_Projects p on b.ProjectId = p.Id  where p.Id = " + project.Id.ToString() + " and e.Mandatory = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                MainExcelModel.UploadEquipmentToExcel(sqlDataReader, project.Name);

                connection.Close();
                return sqlDataReader;
            }
        }
    }
}
