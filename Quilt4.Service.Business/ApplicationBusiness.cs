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
        private readonly IUserAccessBusiness _userAccessBusiness;

        public ApplicationBusiness(IRepository repository, IUserAccessBusiness userAccessBusiness)
        {
            _repository = repository;
            _userAccessBusiness = userAccessBusiness;
        }

        public IEnumerable<Application> GetApplications(Guid projectKey)
        {
            return _repository.GetApplications(projectKey);
        }

        public IEnumerable<Application> GetApplications(string userName, Guid projectKey)
        {
            _userAccessBusiness.AssureAccess(userName, projectKey);
            var result = _repository.GetApplications(projectKey);
            return result;
        }
    }
}