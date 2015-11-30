using System;

namespace Quilt4.Service.Interface.Repository
{
    public interface IUpdateReadRepository
    {
        void CreateUser(string userName);
        void UpdateDashboardPageProject(Guid projectKey);
        void UpdateProjectPageProject(Guid projectKey);
        void UpdateProjectPageApplication(Guid projectKey, Guid applicaitonKey);
        void UpdateProjectPageVersion(Guid projectKey, Guid applicaitonKey, Guid versionKey);
        void UpdateVersionPageVersion(Guid projectKey, Guid applicaitonKey, Guid versionKey);
        //void UpdateVersionPageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        //void UpdateIssueTypePageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        //void UpdateIssueTypePageIssue(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId);
    }
}