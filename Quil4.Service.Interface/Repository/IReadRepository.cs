using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Repository
{
    public interface IReadRepository
    {

        ProjectPageProject GetProject(string userName, Guid projectId);
        ProjectPageProject GetProject(Guid projectId);

        IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId);
        VersionPageVersion GetVersion(string userName, Guid projectId, Guid applicationId, Guid versionId);
        IssueTypePageIssueType GetIssueType(string userName, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        IEnumerable<DashboardPageProject> GetDashboardProjects(string userName);
    }
}