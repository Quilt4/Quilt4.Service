using System;
using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class VersionPageVersionResponse
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string ApplicationId { get; set; }
        public string Version { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public IEnumerable<VersionPageIssueTypeResponse> IssueTypes { get; set; }
    }

    public class VersionPageIssueTypeResponse
    {
        public string Id { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public int Issues { get; set; }
        public string Level { get; set; }
        public DateTime? LastIssue { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
        public string Message { get; set; }
    }
}