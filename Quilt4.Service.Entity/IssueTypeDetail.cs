using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class SessionDetail
    {
        public DateTime LastUsedServerTime { get; set; }
    }

    public class IssueTypeDetail
    {
        public Guid IssueTypeKey { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public int IssueCount { get; set; }
        public string Level { get; set; }
        public DateTime FirstIssue { get; set; }
        public DateTime LastIssue { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
        public string Message { get; set; }
    }
}