using System;
using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IProject
    {
        string Name { get; }
    }

    public interface IGetProjectQueryOutput
    {
        IEnumerable<IProject> Projects { get; }
    }

    public interface IGetProjectQueryInput
    {
        string UserName { get; }
    }

    public interface ICrateUserCommandInput
    {
        string UserName { get; }
    }

    public interface ICreateProjectCommandInput
    {
        string UserName { get; }
        Guid ProjectKey { get; }
        string ProjectName { get; }
        string DashboardColor { get; }
    }
}