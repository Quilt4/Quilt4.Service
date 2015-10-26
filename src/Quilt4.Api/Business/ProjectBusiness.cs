using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Api.Entities;
using Quilt4.Api.Interfaces;

namespace Quilt4.Api.Business
{
    internal class ProjectBusiness : IProjectBusiness
    {
        private readonly IRepository _repository;

        public ProjectBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Project> GetProjects(string username)
        {
            //TODO: Figure out what project the requesting user is allowed to see.
            //Projects where the user is owner and project where the user have been invited
            var projects = _repository.GetProjects();
            return projects;
        }

        public Project GetProject(string username, Guid projectId)
        {
            return GetProjects(username).Single(x => x.ProjectId == projectId);
        }

        public void CreateProject(Project project)
        {
            _repository.SaveProject(project);
        }
    }
}