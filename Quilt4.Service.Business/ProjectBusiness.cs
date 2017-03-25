//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Quilt4.Service.Entity;
//using Quilt4.Service.Interface.Business;
//using Quilt4.Service.Interface.Repository;

//namespace Quilt4.Service.Business
//{
//    public class ProjectBusiness : IProjectBusiness
//    {
//        private readonly IUserAccessBusiness _userAccessBusiness;
//        private readonly IProjectRepository _projectRepository;
//        //private readonly IReadRepository _readRepository;
//        //private readonly IRepository _repository;
//        //private readonly IWriteRepository _writeRepository;

//        public ProjectBusiness(IUserAccessBusiness userAccessBusiness, IProjectRepository projectRepository) //IReadRepository readRepository, IRepository repository, IWriteRepository writeRepository, IUserAccessBusiness userAccessBusiness)
//        {
//            //_readRepository = readRepository;
//            //_repository = repository;
//            //_writeRepository = writeRepository;
//            _userAccessBusiness = userAccessBusiness;
//            _projectRepository = projectRepository;
//        }

//        public ProjectPageProject GetProject(string userName, Guid projectKey)
//        {
//            throw new NotImplementedException();
//            //var result = _readRepository.GetProject(projectKey);
//            //_userAccessBusiness.AssureAccess(userName, result.ProjectKey);
//            //return result;
//        }

//        public IEnumerable<ProjectPageProject> GetAllProjects()
//        {
//            return _projectRepository.GetAllProjects();
//        }

//        public ProjectPageProject GetProject(Guid projectKey)
//        {
//            throw new NotImplementedException();
//            //return _readRepository.GetProject(projectKey);
//        }

//        public IEnumerable<ProjectPageProject> GetProjects(string userName)
//        {
//            return _projectRepository.GetProjects(userName);
//        }

//        public void DeleteProject(Guid projectKey)
//        {
//            throw new NotImplementedException();
//            //_repository.DeleteProject(projectKey);
//        }

//        public IEnumerable<ProjectMember> GetMembers(Guid projectKey)
//        {
//            //TODO: Check access to project
//            var a = _projectRepository.GetProjectUsers(projectKey).Select(x => new ProjectMember(x.UserName, x.EMail, x.Confirmed, x.Role, x.FullName, x.EMail.GetGravatarPath()));
//            var b = _projectRepository.GetProjectInvitation(projectKey);
//            return a.Union(b);
//        }

//        public IEnumerable<ProjectPageVersion> GetVersions(string userName, Guid applicationKey)
//        {
//            throw new NotImplementedException();
//            //var versions = _readRepository.GetVersions(applicationKey).ToArray();
//            //_userAccessBusiness.AssureAccess(userName, versions.Select(x => x.ProjectKey));
//            //return versions;
//        }

//        public void CreateProject(string userName, Guid projectKey, string name, string dashboardColor)
//        {
//            var projectApiKey = RandomUtility.GetRandomString(32);

//            throw new NotImplementedException();
//            //_repository.CreateProject(userName, projectKey, name, DateTime.UtcNow, dashboardColor ?? "blue", projectApiKey);
//            ////_writeRepository.UpdateDashboardPageProject(projectKey);
//            ////_writeRepository.UpdateProjectPageProject(projectKey);
//        }

//        public void UpdateProject(Guid projectKey, string name, string dashboardColor, string userName)
//        {
//            throw new NotImplementedException();
//            //if (_repository.GetProjects(userName).All(x => x.ProjectKey != projectKey))
//            //    throw new InvalidOperationException("The user doesn't have access to the provided project.");

//            //_repository.UpdateProject(projectKey, name, dashboardColor);

//            ////_writeRepository.UpdateDashboardPageProject(projectKey);
//            ////_writeRepository.UpdateProjectPageProject(projectKey);
//        }
//    }
//}