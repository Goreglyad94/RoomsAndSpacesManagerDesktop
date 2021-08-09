using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.SqlRequestModels
{
    class SqlRequestModel : ISqlRequestService
    {
        private const string connectionString = @"Data Source=nt-db01.ukkalita.local;Initial Catalog=M1_Revit;integrated security=True;MultipleActiveResultSets=True";
        public List<List<string>> GetSqlResponse(string request)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(request, connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                List<List<string>> data = new List<List<string>>();
                
                while (sqlDataReader.Read())
                {
                    List<string> row = new List<string>();
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        row.Add(sqlDataReader.GetValue(i).ToString());
                    }

                    data.Add(row);
                    //uploadService.WriteRow(row, workSheet);
                }

                connection.Close();
                return data;
            }
        }
    }
}
