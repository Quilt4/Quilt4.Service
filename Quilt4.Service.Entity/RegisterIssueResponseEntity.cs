using System;

namespace Quilt4.Service.Entity
{
    public class RegisterIssueResponseEntity
    {
        public int Ticket { get; set; }
        public DateTime ServerTime { get; set; }
        public Guid IssueKey { get; set; }
    }
}