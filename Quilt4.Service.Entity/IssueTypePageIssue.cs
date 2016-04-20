using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class IssueTypePageIssue
    {
        public Guid IssueKey { get; set; }
        public DateTime CreationServerTime { get; set; }
        public string UserName { get; set; }
        public string Enviroment { get; set; }
        public IDictionary<string, string> Data { get; set; }
        public IDictionary<string, string> IssueThreadKeys { get; set; }
    }
}