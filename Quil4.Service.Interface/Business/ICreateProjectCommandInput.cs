using System;

namespace Quilt4.Service.Interface.Business
{
    public interface ICreateProjectCommandInput
    {
        string UserName { get; }
        Guid ProjectKey { get; }
        string ProjectName { get; }
        string DashboardColor { get; }
        string ProjectApiKey { get; }
    }
}