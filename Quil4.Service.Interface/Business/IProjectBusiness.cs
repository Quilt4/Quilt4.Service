﻿using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IProjectBusiness
    {
        ProjectPageProject GetProject(string userId, Guid projectId);
        IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId);
        Guid CreateProject(string name, string dashboardColor);
        Guid UpdateProject(Guid projectId, string name, string dashboardColor);
    }
}