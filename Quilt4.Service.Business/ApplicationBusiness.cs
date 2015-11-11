using System;
using System.Collections.Generic;
using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Business
{
    public class ApplicationBusiness : IApplicationBusiness
    {
        private readonly IReadRepository _readRepository;

        public ApplicationBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public IEnumerable<Application> GetApplications(string userId, Guid projectId)
        {
            return _readRepository.GetApplications(userId, projectId);
        }

        public Application GetApplication(string userId, Guid projectId, Guid applicationId)
        {
            return _readRepository.GetApplication(userId, projectId, applicationId);
        }
    }
}