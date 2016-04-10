using System;

namespace Quilt4.Service.Entity
{
    public class RegisterIssueResponseEntity
    {
        public RegisterIssueResponseEntity(Guid issueKey, int ticket, DateTime serverTime, Guid projectKey)
        {
            IssueKey = issueKey;
            Ticket = ticket;
            ServerTime = serverTime;
            ProjectKey = projectKey;
        }

        public int Ticket { get; }
        public DateTime ServerTime { get; }
        public Guid IssueKey { get; }
        public Guid ProjectKey { get; }
    }
}