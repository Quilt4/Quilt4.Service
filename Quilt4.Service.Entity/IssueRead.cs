using System;

namespace Quilt4.Service.Entity
{
    public class IssueRead : IIssueRead
    {
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public string VersionNumber { get; set; }
        public string MachineName { get; set; }
        public int Ticket { get; set; }
        public string StackTrace { get; set; }
        public Guid IssueKey { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
    }
}