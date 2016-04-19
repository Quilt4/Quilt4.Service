using System;
using System.Collections.Generic;

namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class ProjectPageVersionResponse
    {
        public string VersionKey { get; set; }
        public string ProjectKey { get; set; }
        public string ApplicationKey { get; set; }
        public string VersionNumber { get; set; }
        public int SessionCount { get; set; }
        public int IssueTypeCount { get; set; }
        public int IssueCount { get; set; }
        public DateTime FirstSession { get; set; }
        public DateTime LastSession { get; set; }
        public DateTime? LastIssue { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
    }
}