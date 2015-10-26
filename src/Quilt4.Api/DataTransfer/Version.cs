using System;
using System.Collections.Generic;

namespace Quilt4.Api.DataTransfer
{
    public class Version
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string ApplicationId { get; set; }
        public string Name { get; set; }
        public int Sessions { get; set; }
        public int IssueTypes { get; set; }
        public int Issues { get; set; }
        public IEnumerable<string> Enviroments { get; set; } 
        public DateTime Last { get; set; }
    }
}