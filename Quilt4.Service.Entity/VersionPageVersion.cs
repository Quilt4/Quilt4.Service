using System;
using System.Collections.Generic;
using System.Net.Http;

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
}