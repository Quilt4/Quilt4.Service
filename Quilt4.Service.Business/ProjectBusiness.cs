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
        private readonly IDataRepository _repository;
        private readonly IUpdateReadRepository _writeRepository;

        public ProjectBusiness(IReadRepository readRepository, IDataRepository repository, IUpdateReadRepository writeRepository)
        {
            _readRepository = readRepository;
            _repository = repository;
            _writeRepository = writeRepository;
        }

        //public IEnumerable<ProjectPageProject> GetProjects(string userName)
        //{
        //    var response = _readRepository.GetDashboardProjects(userName);
        //    var result = response.Select(x => new ProjectPageProject { Name = x.Name });
        //    return result;
        //}

        //TODO: Revisit
        public ProjectPageProject GetProject(string userId, Guid projectId)
        {
            //return _readRepository.GetProject(userId, projectId);
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId)
        {
            throw new NotImplementedException();
            //return _readRepository.GetVersions(userId, projectId, applicationId);
        }

        public Guid UpdateProject(Guid projectId, string name, string dashboardColor)
        {
            throw new NotImplementedException();
            //_repository.UpdateProject(projectId, name, dashboardColor);

            //_writeRepository.UpdateDashboardPageProject(projectId);
            //_writeRepository.UpdateProjectPageProject(projectId);

            //return projectId;
        }
    }
}