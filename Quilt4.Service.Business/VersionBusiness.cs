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
        private readonly IUserAccessBusiness _userAccessBusiness;

        public VersionBusiness(IRepository repository, IReadRepository readRepository, IUserAccessBusiness userAccessBusiness)
        {
            _repository = repository;
            _readRepository = readRepository;
            _userAccessBusiness = userAccessBusiness;
        }

        public VersionDetail GetVersion(string userName, Guid versionKey)
        {
            var response = _readRepository.GetVersion(versionKey);
            _userAccessBusiness.AssureAccess(userName, response.ProjectKey);
            return response;
        }

        public VersionDetail GetVersion(Guid versionKey)
        {
            var response = _readRepository.GetVersion(versionKey);
            return response;
        }

        public IEnumerable<Version> GetVersions(string userName, Guid applicationKey)
        {
            var result = _repository.GetVersions(applicationKey).ToArray();
            _userAccessBusiness.AssureAccess(userName, result.Select(x => x.ProjectKey));
            return result;
        }

        public IEnumerable<Version> GetVersions(Guid applicationKey)
        {
            return _repository.GetVersions(applicationKey);
        }
    }
}