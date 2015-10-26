using System;
using System.Collections.Generic;

namespace Quilt4.Api.DataTransfer
{
    public class IssueType
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string ApplicationId { get; set; }
        public string VersionId { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public int Issues { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
        public DateTime Last { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

    }
}