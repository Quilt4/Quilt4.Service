using System;

namespace Quilt4.Service.Interface.Repository
{
    public interface IUpdateReadRepository
    {
        void CreateUser(string userName);
        void UpdateDashboardPageProject(Guid projectKey);
        void UpdateProjectPageProject(Guid projectKey);
        //void UpdateProjectPageApplication(Guid projectId, Guid applicaitonId);
        //void UpdateProjectPageVersion(Guid projectId, Guid applicaitonId, Guid versionId);
        //void UpdateVersionPageVersion(Guid projectId, Guid applicaitonId, Guid versionId);
        //void UpdateVersionPageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        //void UpdateIssueTypePageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        //void UpdateIssueTypePageIssue(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId);
    }
}