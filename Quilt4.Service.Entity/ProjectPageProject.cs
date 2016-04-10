using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class ProjectPageProject
    {
        public ProjectPageProject(Guid projectKey, string name, string dashboardColor, string projectApiKey, ProjectPageApplication[] applications)
        {
            ProjectKey = projectKey;
            Name = name;
            DashboardColor = dashboardColor;
            ProjectApiKey = projectApiKey;
            Applications = applications;
        }

        public Guid ProjectKey { get; }
        public string Name { get; }
        public string DashboardColor { get; }
        public string ProjectApiKey { get; }
        public IEnumerable<ProjectPageApplication> Applications { get; }
    }
}