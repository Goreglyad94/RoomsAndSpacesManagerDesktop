﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.Interfaces
{
    interface IUploadService
    {
        bool UploadAllUssues(int projectId, string projectName);
        bool UploadRoomProgram(int projectId, string projectName);
    }
}
