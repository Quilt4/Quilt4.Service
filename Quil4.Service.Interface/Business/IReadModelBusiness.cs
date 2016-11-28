using System;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IReadModelBusiness
    {
        void AddIssue(Guid responseIssueKey);
        IIssueRead GetIssue(Guid responseIssueKey);
    }
}