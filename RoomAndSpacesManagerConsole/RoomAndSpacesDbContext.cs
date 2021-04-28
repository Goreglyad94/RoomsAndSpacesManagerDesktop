using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesManagerConsole
{
    class RoomAndSpacesDbContext : DbContext
    {
        public RoomAndSpacesDbContext()
        {
            this.Database.Connection.ConnectionString = @"Data Source=nt-db01.ukkalita.local;Initial Catalog=M1_Revit;integrated security=True;MultipleActiveResultSets=True";
        }

    }
}
