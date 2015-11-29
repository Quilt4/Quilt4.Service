using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Repository
{
    public interface IReadRepository
    {
        IEnumerable<DashboardPageProject> GetDashboardProjects(string userName);

        //TODO: Revisit
        ProjectPageProject GetProject(string userId, Guid projectId);
        IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId);
        VersionPageVersion GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId);
        IssueTypePageIssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
    }
}