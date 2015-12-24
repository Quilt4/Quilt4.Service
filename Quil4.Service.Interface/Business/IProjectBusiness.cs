using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IProjectBusiness
    {
        ProjectPageProject GetProject(string userName, Guid projectId);
        IEnumerable<ProjectPageProject> GetProjects(string userName);
        ProjectPageProject GetProject(Guid projectId);
        void CreateProject(string userName, Guid projectKey, string name, string dashboardColor);
        void UpdateProject(Guid projectKey, string name, string dashboardColor, string userName);
        void DeleteProject(Guid projectKey);

        //TODO: Revisit
        IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId);
    }
}