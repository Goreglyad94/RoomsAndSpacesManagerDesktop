using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels.Base
{
    class MainDatabaseContext
    {
        public RoomAndSpacesDbContext context;
        public MainDatabaseContext()
        {
            context = new RoomAndSpacesDbContext();
        }
    }
}
