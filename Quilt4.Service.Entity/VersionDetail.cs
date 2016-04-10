using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class VersionDetail
    {
        public Guid VersionKey { get; set; }
        public Guid ProjectKey { get; set; }
        public Guid ApplicationKey { get; set; }
        public string VersionNumber { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public IEnumerable<IssueTypeDetail> IssueTypes { get; set; }
    }
}