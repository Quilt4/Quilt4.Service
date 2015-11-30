using System;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Entity
{
    public class Project : IProject
    {
        public Project(Guid projectKey, string name, string dashboardColor, string projectApiKey)
        {
            ProjectKey = projectKey;
            Name = name;
            DashboardColor = dashboardColor;
            ProjectApiKey = projectApiKey;
        }

        public Guid ProjectKey { get; }
        public string Name { get; }
        public string DashboardColor { get; }
        public string ProjectApiKey { get; }
    }
}