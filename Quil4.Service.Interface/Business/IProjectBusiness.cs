using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IProjectBusiness
    {
         //TODO: Revisit
         ProjectPageProject GetProject(string userId, Guid projectId);
        IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId);
        Guid UpdateProject(Guid projectId, string name, string dashboardColor);
    }
}