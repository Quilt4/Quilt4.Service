using System;
using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class IssueTypePageIssueTypeResponse
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string ApplicationId { get; set; }
        public string VersionId { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public IEnumerable<IssueTypePageIssueResponse> Issues { get; set; }
    }

    public class IssueTypePageIssueResponse
    {
        public string Id { get; set; }
        public DateTime Time { get; set; }
        public string User { get; set; }
        public string Enviroment { get; set; }
        public IDictionary<string, string> Data { get; set; }
    }
}