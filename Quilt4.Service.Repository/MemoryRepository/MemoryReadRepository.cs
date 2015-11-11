using System;
using System.Collections.Generic;
using System.Linq;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;
using Version = Quilt4.Service.Entity.Version;

namespace Quilt4.Service.Repository.MemoryRepository
{
    public class MemoryReadRepository : IReadRepository
    {
        private readonly IEnumerable<Application> _applications = new List<Application>();
        private readonly IEnumerable<Issue> _issues = new List<Issue>();
        private readonly IEnumerable<IssueType> _issueTypes = new List<IssueType>();
        private readonly IEnumerable<Project> _projects = new List<Project>();
        private readonly IEnumerable<Version> _versions = new List<Version>();

        public IEnumerable<Project> GetProjects(string userId)
        {
            return _projects;
        }

        public Project GetProject(string userId, Guid projectId)
        {
            return _projects.FirstOrDefault(x => x.Id == projectId);
        }

        public IEnumerable<Application> GetApplications(string userId, Guid projectId)
        {
            return _applications.Where(x => x.ProjectId == projectId);
        }

        public Application GetApplication(string userId, Guid projectId, Guid applicationId)
        {
            return _applications.SingleOrDefault(x => x.ProjectId == projectId && x.Id == applicationId);
        }

        public IEnumerable<Version> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            return _versions.Where(x => x.ProjectId == projectId && x.ApplicationId == applicationId);
        }

        public Version GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId)
        {
            return
                _versions.SingleOrDefault(
                    x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.Id == versionId);
        }

        public IEnumerable<IssueType> GetIssueTypes(string userId, Guid projectId, Guid applicationId, Guid versionId)
        {
            return
                _issueTypes.Where(
                    x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId);
        }

        public IssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId,
            Guid issueTypeId)
        {
            return
                _issueTypes.SingleOrDefault(
                    x =>
                        x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId &&
                        x.Id == issueTypeId);
        }

        public IEnumerable<Issue> GetIssues(string userId, Guid projectId, Guid applicationId, Guid versionId,
            Guid issueTypeId)
        {
            return _issues.Where(x =>
                x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId &&
                x.IssueTypeId == issueTypeId);
        }

        public Issue GetIssue(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId)
        {
            return _issues.SingleOrDefault(x =>
                 x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId &&
                 x.IssueTypeId == issueTypeId && x.Id == issueId);
        }
    }
}