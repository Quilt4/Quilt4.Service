using System;

namespace Quilt4.Service.Interface.Business
{
    public interface IProject
    {
        string Name { get; }
        Guid ProjectKey { get; }
        string DashboardColor { get; }
    }
}