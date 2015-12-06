using System;
using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class RegisterIssueRequest
    {
        public string Id { get; set; } //Guid
        public string SessionId { get; set; }
        public DateTime ClientTime { get; set; }
        public IDictionary<string, string> Data { get; set; }
        public IssueTypeRequest IssueType { get; set; }
        public string IssueThreadId { get; set; } //Nullable guid
        public string UserHandle { get; set; }
        public string UserInput { get; set; }
        public string ClientToken { get; set; }
    }
}