using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class Issue
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid VersionId { get; set; }
        public Guid IssueTypeId { get; set; }
        public string Enviroment { get; set; }
        public string User { get; set; }
        public DateTime Time { get; set; }
        public IDictionary<string, string> Data { get; set; }

    }
}