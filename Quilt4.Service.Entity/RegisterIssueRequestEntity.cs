using System;

namespace Quilt4.Service.Entity
{
    public class RegisterIssueRequestEntity
    {
        public Guid IssueKey { get; set; }
        public string SessionKey { get; set; }
        public DateTime ClientTime { get; set; }
        public IssueTypeRequestEntity IssueType { get; set; }
        public Guid? IssueThreadId { get; set; }
        public string UserHandle { get; set; }
        public string UserInput { get; set; }
        public string Level { get; set; }
    }
}