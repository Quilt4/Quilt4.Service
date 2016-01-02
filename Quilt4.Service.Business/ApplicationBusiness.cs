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

        public IEnumerable<Application> GetApplications(string userName, Guid projectKey)
        {
            return _repository.GetApplications(userName, projectKey);
        }

        public ApplicationBusiness(IRepository repository)
        {
            _repository = repository;
        }
    }
}