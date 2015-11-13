using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class ProjectPageVersion
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ApplicationId { get; set; }
        public string Version { get; set; }
        public int Sessions { get; set; }
        public int IssueTypes { get; set; }
        public int Issues { get; set; }
        public DateTime? Last { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
    }
}