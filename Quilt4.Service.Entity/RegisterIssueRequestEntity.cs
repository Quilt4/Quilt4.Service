using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class RegisterIssueRequestEntity
    {
        public Guid IssueKey { get; set; }
        public string SessionToken { get; set; }
        public DateTime ClientTime { get; set; }
        public IDictionary<string, string> Data { get; set; }
        public IssueTypeRequestEntity IssueType { get; set; }
        public Guid? IssueThreadId { get; set; }
        public string UserHandle { get; set; }
        public string UserInput { get; set; }
    }
}