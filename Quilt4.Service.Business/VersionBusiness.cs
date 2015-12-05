﻿using System;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class VersionBusiness : IVersionBusiness
    {
        private readonly IReadRepository _readRepository;

        public VersionBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public VersionPageVersion GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId)
        {
            return _readRepository.GetVersion(userId, projectId, applicationId, versionId);
        }
    }
}