using System;

namespace Quilt4.Service.Entity
{
    public class DashboardPageProject
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public int VersionCount { get; set; }
        public int SessionCount { get; set; }
        public int IssueTypeCount { get; set; }
        public int IssueCount { get; set; }
        public string DashboardColor { get; set; }
    }
}