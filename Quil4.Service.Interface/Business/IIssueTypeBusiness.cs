using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IIssueTypeBusiness
    {
        IEnumerable<IssueType> GetIssueTypes(string userId, Guid projectId, Guid applicationId, Guid versionId);
        IssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
    }
}