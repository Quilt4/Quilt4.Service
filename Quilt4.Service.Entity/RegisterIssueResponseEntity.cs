using System;

namespace Quilt4.Service.Entity
{
    public class RegisterIssueResponseEntity
    {
        public RegisterIssueResponseEntity(Guid issueKey, int ticket, DateTime serverTime, Guid projectKey, Guid issueTypeKey, string sessionKey)
        {
            SessionKey = sessionKey;
            IssueKey = issueKey;
            Ticket = ticket;
            ServerTime = serverTime;
            ProjectKey = projectKey;
            IssueTypeKey = issueTypeKey;
        }

        public int Ticket { get; }
        public DateTime ServerTime { get; }
        public Guid IssueKey { get; }
        public Guid ProjectKey { get; }
        public Guid IssueTypeKey { get; }
        public string SessionKey { get; }
    }
}