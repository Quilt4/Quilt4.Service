using System;
using System.Collections.Generic;
using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;
using Version = Quilt4.Service.Entity.Version;

namespace Quilt4.Service.Business
{
    public class VersionBusiness : IVersionBusiness
    {
        private readonly IReadRepository _readRepository;
        public VersionBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public IEnumerable<Version> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            return _readRepository.GetVersions(userId, projectId, applicationId);
        }

        public Version GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId)
        {
            return _readRepository.GetVersion(userId, projectId, applicationId, versionId);
        }
    }
}