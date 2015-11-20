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
        private readonly IRepository _repository;
        private readonly IWriteRepository _writeRepository;

        public ProjectBusiness(IReadRepository readRepository, IRepository repository, IWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _repository = repository;
            _writeRepository = writeRepository;
        }

        public ProjectPageProject GetProject(string userId, Guid projectId)
        {
            return _readRepository.GetProject(userId, projectId);
        }

        public IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            return _readRepository.GetVersions(userId, projectId, applicationId);
        }

        public Guid CreateProject(string name, string dashboardColor)
        {
            var projectId =  _repository.CreateProject(name, dashboardColor);

            _writeRepository.UpdateDashboardPageProject(projectId);
            _writeRepository.UpdateProjectPageProject(projectId);

            return projectId;
        }

        public Guid UpdateProject(Guid projectId, string name, string dashboardColor)
        {
            _repository.UpdateProject(projectId, name, dashboardColor);

            _writeRepository.UpdateDashboardPageProject(projectId);
            _writeRepository.UpdateProjectPageProject(projectId);

            return projectId;
        }
    }
}