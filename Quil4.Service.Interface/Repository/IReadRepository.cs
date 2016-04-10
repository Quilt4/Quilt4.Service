using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Repository
{
    public interface IReadRepository
    {
        ProjectPageProject GetProject(string userName, Guid projectKey);
        ProjectPageProject GetProject(Guid projectKey);
        IEnumerable<ProjectPageVersion> GetVersions(string userName, Guid projectKey, Guid applicationKey);
        VersionPageVersion GetVersion(string userName, Guid projectKey, Guid applicationKey, Guid versionKey);
        IssueTypePageIssueType GetIssueType(string userName, Guid issueTypeKey);
        IEnumerable<DashboardPageProject> GetDashboardProjects(string userName);
    }
}