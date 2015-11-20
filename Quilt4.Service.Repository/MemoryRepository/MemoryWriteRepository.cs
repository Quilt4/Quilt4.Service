using System;
using Quil4.Service.Interface.Repository;

namespace Quilt4.Service.Repository.MemoryRepository
{
    public class MemoryWriteRepository : IWriteRepository
    {
        public void UpdateDashboardPageProject(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProjectPageProject(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProjectPageApplication(Guid projectId, Guid applicaitonId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProjectPageVersion(Guid projectId, Guid applicaitonId, Guid versionId)
        {
            throw new NotImplementedException();
        }

        public void UpdateVersionPageVersion(Guid projectId, Guid applicaitonId, Guid versionId)
        {
            throw new NotImplementedException();
        }

        public void UpdateVersionPageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateIssueTypePageIssueType(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateIssueTypePageIssue(Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId)
        {
            throw new NotImplementedException();
        }
    }
}