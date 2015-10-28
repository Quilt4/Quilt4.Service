using System;

namespace Quilt4.Api.DataTransfer
{
    public class ProjectResponse
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public int VersionCount { get; set; }
        public int SessionCount { get; set; }
        public int IssueTypeCount { get; set; }
        public int IssueCount { get; set; }
        public string DashboardColor { get; set; }
    }
}