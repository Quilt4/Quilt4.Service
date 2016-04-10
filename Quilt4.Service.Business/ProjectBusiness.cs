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

        public ProjectPageProject GetProject(string userName, Guid projectId)
        {
            return _readRepository.GetProject(userName, projectId);
        }

        public IEnumerable<ProjectPageProject> GetAllProjects()
        {
            return _repository.GetAllProjects();
        }

        public ProjectPageProject GetProject(Guid projectKey)
        {
            return _readRepository.GetProject(projectKey);
        }

        public IEnumerable<ProjectPageProject> GetProjects(string userName)
        {
            return _repository.GetProjects(userName);
        }

        public void DeleteProject(Guid projectKey)
        {
            _repository.DeleteProject(projectKey);
        }

        public IEnumerable<ProjectMember> GetMembers(Guid projectKey)
        {
            //TODO: Check access to project
            var a = _repository.GetProjectUsers(projectKey).Select(x => new ProjectMember(x.UserName, x.EMail, x.Confirmed, x.Role, x.FullName, x.EMail.GetGravatarPath()));
            var b = _repository.GetProjectInvitation(projectKey);
            return a.Union(b);
        }

        public IEnumerable<ProjectPageVersion> GetVersions(string userName, Guid applicationKey)
        {
            var versions = _readRepository.GetVersions(applicationKey).ToArray();
            AssureUserAccess(userName, versions);
            return versions;
        }

        private void AssureUserAccess(string userName, ProjectPageVersion[] versions)
        {
            Guid? lastProjectKey = null;
            string[] projectUsers = null;
            foreach (var version in versions)
            {
                if (lastProjectKey != version.ProjectKey)
                {
                    lastProjectKey = version.ProjectKey;
                    projectUsers = _readRepository.GetProjectUsers(lastProjectKey.Value).ToArray();
                }
                if (projectUsers.All(x => x != userName)) throw new InvalidOperationException();
            }
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
            if (_repository.GetProjects(userName).All(x => x.ProjectKey != projectKey))
                throw new InvalidOperationException("The user doesn't have access to the provided project.");

            _repository.UpdateProject(projectKey, name, dashboardColor);

            _writeRepository.UpdateDashboardPageProject(projectKey);
            _writeRepository.UpdateProjectPageProject(projectKey);
        }
    }
}