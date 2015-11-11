using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class IssueType
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid VersionId { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public int Issues { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
        public DateTime Last { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}