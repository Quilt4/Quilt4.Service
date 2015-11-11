using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Project> GetProjects(string userId)
        {
            return _readRepository.GetProjects(userId);
        }

        public Project GetProject(string userId, Guid projectId)
        {
            return _readRepository.GetProject(userId, projectId);
        }
    }
}