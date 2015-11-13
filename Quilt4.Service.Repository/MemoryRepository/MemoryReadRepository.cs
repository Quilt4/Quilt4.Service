using System;
using System.Collections.Generic;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Repository.MemoryRepository
{
    public class MemoryReadRepository : IReadRepository
    {
        public ProjectPageProject GetProject(string userId, Guid projectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            throw new NotImplementedException();
        }

        public VersionPageVersion GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId)
        {
            throw new NotImplementedException();
        }

        public IssueTypePageIssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId,
            Guid issueTypeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DashboardPageProject> GetDashboardProjects(string userId)
        {
            throw new NotImplementedException();
        }
    }
}