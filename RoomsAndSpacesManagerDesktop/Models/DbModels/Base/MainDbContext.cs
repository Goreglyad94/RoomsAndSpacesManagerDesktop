using RoomsAndSpacesManagerDesktop.Data.DataBaseContext;

namespace RoomsAndSpacesManagerDesktop.Models.DbModels.Base
{
    public class MainDbContext
    {
        protected RoomAndSpacesDbContext context;
        public MainDbContext()
        {
            context = new RoomAndSpacesDbContext();
        }
    }
}
