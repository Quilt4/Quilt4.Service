using System;
using System.Collections.Generic;
using System.Linq;
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

        public VersionDetail GetVersion(string userName, Guid versionKey)
        {
            var response = _readRepository.GetVersion(versionKey);

            var projectUsers = _readRepository.GetProjectUsers(response.ProjectKey);
            if (projectUsers.All(x => x != userName)) throw new InvalidOperationException("The user doesn't have access to the provided project.");

            return response;
        }

        public VersionDetail GetVersion(Guid versionKey)
        {
            var response = _readRepository.GetVersion(versionKey);
            return response;
        }

        public IEnumerable<Version> GetVersions(string userName, Guid applicationKey)
        {
            return _repository.GetVersions(userName, applicationKey);
        }

        public IEnumerable<Version> GetVersions(Guid applicationKey)
        {
            return _repository.GetVersions(applicationKey);
        }
    }
}