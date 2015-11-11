using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Version = Quilt4.Service.Entity.Version;

namespace Quil4.Service.Interface.Repository
{
    public interface IReadRepository
    {
        IEnumerable<Project> GetProjects(string userId);
        Project GetProject(string userId, Guid projectId);
        IEnumerable<Application> GetApplications(string userId, Guid projectId);
        Application GetApplication(string userId, Guid projectId, Guid applicationId);
        IEnumerable<Version> GetVersions(string userId, Guid projectId, Guid applicationId);
        Version GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId);
        IEnumerable<IssueType> GetIssueTypes(string userId, Guid projectId, Guid applicationId, Guid versionId);
        IssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        IEnumerable<Issue> GetIssues(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        Issue GetIssue(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId);
    }
}
