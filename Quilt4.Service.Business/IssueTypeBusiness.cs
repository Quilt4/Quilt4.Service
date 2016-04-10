using System;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class IssueTypeBusiness : IIssueTypeBusiness
    {
        private readonly IReadRepository _readRepository;

        public IssueTypeBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public IssueTypePageIssueType GetIssueType(string userName, Guid issueTypeKey)
        {
            return _readRepository.GetIssueType(userName, issueTypeKey);
        }
    }
}