using System;

namespace Quilt4.Service.Entity
{
    public class ProjectPageProject
    {
        public ProjectPageProject(Guid id, string name, string dashboardColor, string clientToken, ProjectPageApplication[] applications)
        {
            Id = id;
            Name = name;
            DashboardColor = dashboardColor;
            ClientToken = clientToken;
            Applications = applications;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string DashboardColor { get; }
        public string ClientToken { get; }
        public ProjectPageApplication[] Applications { get; }
    }
}