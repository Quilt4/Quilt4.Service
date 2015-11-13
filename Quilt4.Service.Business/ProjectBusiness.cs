using System;
using System.Collections.Generic;
using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Business
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly IReadRepository _readRepository;

        public ProjectBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public ProjectPageProject GetProject(string userId, Guid projectId)
        {
            return _readRepository.GetProject(userId, projectId);
        }

        public IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            return _readRepository.GetVersions(userId, projectId, applicationId);
        }
    }
}