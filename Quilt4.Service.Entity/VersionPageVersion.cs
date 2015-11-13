using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class VersionPageVersion
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ApplicationId { get; set; }
        public string Version { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public IEnumerable<VersionPageIssueType> IssueTypes { get; set; }
    }

    public class VersionPageIssueType
    {
        public Guid Id { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public int Issues { get; set; }
        public string Level { get; set; }
        public DateTime? LastIssue { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
    }
}