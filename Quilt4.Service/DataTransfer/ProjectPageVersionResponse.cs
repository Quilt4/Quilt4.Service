using System;
using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class ProjectPageVersionResponse
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string ApplicationId { get; set; }
        public string Version { get; set; }
        public int Sessions { get; set; }
        public int IssueTypes { get; set; }
        public int Issues { get; set; }
        public DateTime? Last { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
    }
}