﻿using System;
using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Business
{
    public class IssueTypeBusiness : IIssueTypeBusiness
    {
        private readonly IReadRepository _readRepository;

        public IssueTypeBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public IssueTypePageIssueType GetIssueType(string userId, Guid projectId, Guid applicationId, Guid versionId,
            Guid issueTypeId)
        {
            return _readRepository.GetIssueType(userId, projectId, applicationId, versionId, issueTypeId);
        }
    }
}