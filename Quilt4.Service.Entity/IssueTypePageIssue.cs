using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class IssueTypePageIssue
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string User { get; set; }
        public string Enviroment { get; set; }
        public IDictionary<string, string> Data { get; set; }
    }
}