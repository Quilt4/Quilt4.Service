using System;

namespace Quilt4.Service.Interface.Business
{
    public interface IIssueTypeBusiness
    {
        IIssueTypePageIssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId);
    }
}