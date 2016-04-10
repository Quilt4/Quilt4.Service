using System;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class IssueTypeBusiness : IIssueTypeBusiness
    {
        private readonly IReadRepository _readRepository;
        private readonly IUserAccessBusiness _userAccessBusiness;

        public IssueTypeBusiness(IReadRepository readRepository, IUserAccessBusiness userAccessBusiness)
        {
            _readRepository = readRepository;
            _userAccessBusiness = userAccessBusiness;
        }

        public IssueTypePageIssueType GetIssueType(string userName, Guid issueTypeKey)
        {
            var result = _readRepository.GetIssueType(issueTypeKey);
            _userAccessBusiness.AssureAccess(userName, result.ProjectId);
            return result;
        }
    }
}