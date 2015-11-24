using System;

namespace Quilt4.Service.Interface.Repository
{
    public interface IWriteRepository
    {
        void UpdateDashboardPageProject(Guid projectId);
        void UpdateProjectPageProject(Guid projectId);
        void UpdateProjectPageApplication(Guid projectId, Guid applicaitonId);
        void UpdateProjectPageVersion(Guid projectId, Guid applicaitonId, Guid versionId);
        void UpdateVersionPageVersion(Guid projectId, Guid applicaitonId, Guid versionId);
        void UpdateVersionPageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        void UpdateIssueTypePageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        void UpdateIssueTypePageIssue(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId);
    }
}