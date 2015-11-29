using System;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Entity
{
    public class DashboardPageProject : IDashboardPageProject
    {
        public DashboardPageProject(Guid projectKey, string name, int versionCount, int sessionCount, int issueTypeCount, int issueCount, string dashboardColor)
        {
            ProjectKey = projectKey;
            Name = name;
            VersionCount = versionCount;
            SessionCount = sessionCount;
            IssueTypeCount = issueTypeCount;
            IssueCount = issueCount;
            DashboardColor = dashboardColor;
        }

        public Guid ProjectKey { get; }
        public string Name { get; }
        public int VersionCount { get; }
        public int SessionCount { get; }
        public int IssueTypeCount { get; }
        public int IssueCount { get; }
        public string DashboardColor { get; }
    }
}