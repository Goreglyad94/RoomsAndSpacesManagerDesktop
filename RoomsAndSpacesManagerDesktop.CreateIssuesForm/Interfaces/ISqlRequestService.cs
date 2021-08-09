using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces
{
    public interface ISqlRequestService
    {
        List<List<string>> GetSqlResponse(string request);
    }
}
