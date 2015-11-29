using System;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Entity
{
    public class Project : IProject
    {
        public Project(Guid projectKey, string name, string dashboardColor)
        {
            ProjectKey = projectKey;
            Name = name;
            DashboardColor = dashboardColor;
        }

        public Guid ProjectKey { get; }
        public string Name { get; }
        public string DashboardColor { get; }
    }
}