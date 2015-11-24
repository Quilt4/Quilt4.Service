using System;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IIssueTypeBusiness
    {
        IssueTypePageIssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
    }
}