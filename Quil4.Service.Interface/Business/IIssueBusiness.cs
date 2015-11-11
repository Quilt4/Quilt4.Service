using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IIssueBusiness
    {
        IEnumerable<Issue> GetIssues(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
        Issue GetIssue(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId);
    }
}