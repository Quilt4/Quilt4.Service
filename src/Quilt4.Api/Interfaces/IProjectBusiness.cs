using System;
using System.Collections.Generic;
using Quilt4.Api.Entities;

namespace Quilt4.Api.Interfaces
{
    public interface IProjectBusiness
    {
        IEnumerable<Project> GetProjects(string username);
        Project GetProject(string username, Guid projectId);
        void CreateProject(Project project);
    }
}