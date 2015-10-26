using System;

namespace Quilt4.Api.Entities
{
    public class Project
    {
        public Project(Guid projectId, string projectName, string ownerName, Version[] versions, Session[] sessions, IssueType[] issueTypes, string dashboardColor)
        {
            OwnerName = ownerName;
            IssueTypes = issueTypes;
            DashboardColor = dashboardColor;
            Sessions = sessions;
            Versions = versions;
            Name = projectName;
            ProjectId = projectId;
        }

        public Guid ProjectId { get; }
        public string Name { get; }
        public Version[] Versions { get; }
        public Session[] Sessions { get; }
        public IssueType[] IssueTypes { get; }
        public string DashboardColor { get; }
        public string OwnerName { get; }
    }
}