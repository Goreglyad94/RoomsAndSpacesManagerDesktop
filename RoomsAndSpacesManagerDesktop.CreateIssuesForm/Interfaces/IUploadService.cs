using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces
{
    public interface IUploadService
    {
        bool UploadToExcel(RoomNameDto roomNameDto);
    }
}
