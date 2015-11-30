using System;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Interface.Repository
{
    public interface IReadRepository
    {
        IDashboardPageProject[] GetDashboardProjects(string userName);
        IProjectPageProject GetProject(string userName, Guid projectKey);

        //TODO: Revisit
        //IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId);
        //VersionPageVersion GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId);
        //IssueTypePageIssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
    }
}