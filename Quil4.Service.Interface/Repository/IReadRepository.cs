using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Repository
{
    public interface IReadRepository
    {
        ProjectPageProject GetProject(string userId, Guid projectId);
        IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId);
        VersionPageVersion GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId);

        IssueTypePageIssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId,
            Guid issueTypeId);

        IEnumerable<DashboardPageProject> GetDashboardProjects(string userId);
    }
}