using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class ProjectPageVersion
    {
        public Guid VersionKey { get; set; }
        public Guid ProjectKey { get; set; }
        public Guid ApplicationKey { get; set; }
        public string Version { get; set; }
        public int SessionCount { get; set; }
        public int IssueTypeCount { get; set; }
        public int IssueCount { get; set; }
        public DateTime FirstSession { get; set; }
        public DateTime LastSession { get; set; }
        public DateTime? LastIssue { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
    }
}