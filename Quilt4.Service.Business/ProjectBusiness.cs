using System;
using System.Collections.Generic;
using System.Linq;
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

        //public ICommandHandler<T> GetCommandHandler<T>()
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<ProjectPageProject> GetProjects(string userName)
        {
            var response = _readRepository.GetDashboardProjects(userName);
            var result = response.Select(x => new ProjectPageProject { Name = x.Name });
            return result;
        }

        //TODO: Revisit
        public ProjectPageProject GetProject(string userId, Guid projectId)
        {
            return _readRepository.GetProject(userId, projectId);
        }

        public IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            return _readRepository.GetVersions(userId, projectId, applicationId);
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