using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface ICreateProjectCommandInput
    {
        string UserName { get; }
        Guid ProjectKey { get; }
        string ProjectName { get; }
        string DashboardColor { get; }
    }

    //public interface ICommandHandler<T>
    //{
    //    void Handle(T input);
    //}

    public interface IProjectBusiness
    {
        IEnumerable<ProjectPageProject> GetProjects(string userName);
        //void CreateProject(string userName, Guid projectKey, string projectName, string dashboardColor);
        //ICommandHandler<T> GetCommandHandler<T>();

         //TODO: Revisit
         ProjectPageProject GetProject(string userId, Guid projectId);
        IEnumerable<ProjectPageVersion> GetVersions(string userId, Guid projectId, Guid applicationId);
        Guid UpdateProject(Guid projectId, string name, string dashboardColor);
    }
}