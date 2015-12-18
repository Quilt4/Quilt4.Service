using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

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


        public ProjectPageProject GetProject(string userName, Guid projectId)
        {
            return _readRepository.GetProject(userName, projectId);
        }

        public ProjectPageProject GetProject(Guid projectId)
        {
            return _readRepository.GetProject(projectId);
        }

        public IEnumerable<ProjectPageProject> GetProjects(string userName)
        {
            return _repository.GetProjects(userName);
        }

        public void DeleteProject(Guid projectKey)
        {
            _repository.DeleteProject(projectKey);
        }

        public IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            return _readRepository.GetVersions(userId, projectId, applicationId);
        }

        public void CreateProject(string userName, Guid projectKey, string name, string dashboardColor)
        {
            var projectApiKey = RandomUtility.GetRandomString(32);

            _repository.CreateProject(userName, projectKey, name, DateTime.UtcNow, dashboardColor ?? "blue", projectApiKey);
            _writeRepository.UpdateDashboardPageProject(projectKey);
            _writeRepository.UpdateProjectPageProject(projectKey);
        }

        public void UpdateProject(Guid projectKey, string name, string dashboardColor, string userName)
        {
            _repository.UpdateProject(projectKey, name, dashboardColor, DateTime.UtcNow, userName);

            _writeRepository.UpdateDashboardPageProject(projectKey);
            _writeRepository.UpdateProjectPageProject(projectKey);
        }
    }
}