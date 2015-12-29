using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IIssueBusiness
    {
        RegisterIssueResponseEntity RegisterIssue(RegisterIssueRequestEntity toRegisterIssueRequestEntity);
        IEnumerable<IssueType> GetIssueTypeList(string userName, Guid versionKey);
    }
}