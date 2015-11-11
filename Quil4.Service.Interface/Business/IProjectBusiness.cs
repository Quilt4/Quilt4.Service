using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IProjectBusiness
    {
        IEnumerable<Project> GetProjects(string userId);
        Project GetProject(string userId, Guid projectId);
    }
}