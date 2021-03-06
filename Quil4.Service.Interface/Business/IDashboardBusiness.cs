﻿using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IDashboardBusiness
    {
        IEnumerable<DashboardPageProject> GetProjects(string userName);
        IEnumerable<ProjectInvitation> GetInvitations(string userName);
    }
}