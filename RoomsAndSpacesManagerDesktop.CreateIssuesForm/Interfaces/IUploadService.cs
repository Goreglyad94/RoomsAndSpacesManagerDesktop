using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces
{
    interface IUploadService
    {
        bool UploadAllUssues(int projectId, string projectName);
        bool UploadRoomProgram(ProjectDto project, double k);
        bool UploadRoomSummary(List<BuildingDto> project, double k, ref ExcelWorksheet worksheet);
    }
}
