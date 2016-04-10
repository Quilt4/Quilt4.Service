using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IProjectBusiness
    {
        ProjectPageProject GetProject(string userName, Guid projectKey);
        IEnumerable<ProjectPageProject> GetAllProjects();
        IEnumerable<ProjectPageProject> GetProjects(string userName);
        ProjectPageProject GetProject(Guid projectKey);
        void CreateProject(string userName, Guid projectKey, string name, string dashboardColor);
        void UpdateProject(Guid projectKey, string name, string dashboardColor, string userName);
        void DeleteProject(Guid projectKey);
        IEnumerable<ProjectMember> GetMembers(Guid projectKey);
        IEnumerable<ProjectPageVersion> GetVersions(string userName, Guid applicationKey);
    }
}