using System;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IIssueTypeBusiness
    {
        IssueTypePageIssueType GetIssueType(string userName, Guid issueTypeKey);
    }
}