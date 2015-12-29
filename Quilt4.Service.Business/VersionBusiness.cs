using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class ApplicationBusiness : IApplicationBusiness
    {
        private readonly IRepository _repository;

        public ApplicationBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Application> GetApplications(string userName, Guid projectKey)
        {
            return _repository.GetApplications(userName, projectKey);
        }
    }

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
    }
}