using System;
using System.Collections.Generic;
using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Business
{
    public class IssueBusiness : IIssueBusiness
    {
        private readonly IReadRepository _readRepository;
        public IssueBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public IEnumerable<Issue> GetIssues(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId)
        {
            return _readRepository.GetIssues(userId, projectId, applicationId, versionId, issueTypeId);
        }

        public Issue GetIssue(string userId, Guid projectId, Guid applicationId, Guid versionId, Guid issueTypeId, Guid issueId)
        {
            return _readRepository.GetIssue(userId, projectId, applicationId, versionId, issueTypeId, issueId);
        }
    }
}