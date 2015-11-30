using System;

namespace Quilt4.Service.Interface.Business
{
    public interface IGetProjectQueryInput
    {
        string UserName { get; }
        Guid ProjectKey { get; }
    }

    public interface IGetProjectsQueryInput
    {
        string UserName { get; }
    }
}