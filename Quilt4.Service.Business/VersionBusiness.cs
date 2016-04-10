using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;
using Version = Quilt4.Service.Entity.Version;

namespace Quilt4.Service.Business
{
    public class VersionBusiness : IVersionBusiness
    {
        private readonly IRepository _repository;
        private readonly IReadRepository _readRepository;

        public VersionBusiness(IRepository repository, IReadRepository readRepository)
        {
            _repository = repository;
            _readRepository = readRepository;
        }

        public VersionPageVersion GetVersion(string userName, Guid projectId, Guid applicationId, Guid versionId)
        {
            return _readRepository.GetVersion(userName, projectId, applicationId, versionId);
        }

        public IEnumerable<Entity.Version> GetVersions(string userName, Guid applicationKey)
        {
            return _repository.GetVersions(userName, applicationKey);
        }

        public IEnumerable<Version> GetVersions(Guid applicationKey)
        {
            return _repository.GetVersions(applicationKey);
        }
    }
}