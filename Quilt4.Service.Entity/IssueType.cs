using System;

namespace Quilt4.Service.Entity
{
    public class IssueType
    {
        public IssueType(Guid issueTypeKey, Guid projectKey, Guid versionKey, string type, string level, string message, string stackTrace, int ticket, DateTime creationServerTime)
        {
            IssueTypeKey = issueTypeKey;
            ProjectKey = projectKey;
            VersionKey = versionKey;
            Type = type;
            Level = level;
            Message = message;
            StackTrace = stackTrace;
            Ticket = ticket;
            CreationServerTime = creationServerTime;
        }

        public Guid IssueTypeKey { get; }
        public Guid ProjectKey { get; }
        public Guid VersionKey { get; }
        public string Type { get; }
        public string Level { get; }
        public string Message { get; }
        public string StackTrace { get; }
        public int Ticket { get; }
        public DateTime CreationServerTime { get; }
    }
}