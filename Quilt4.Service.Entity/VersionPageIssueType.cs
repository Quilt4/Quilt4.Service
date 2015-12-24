using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class VersionPageIssueType
    {
        public Guid Id { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public int Issues { get; set; }
        public string Level { get; set; }
        public DateTime? LastIssue { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
        public string Message { get; set; }
    }
}