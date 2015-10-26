using System;
using System.Collections.Generic;

namespace Quilt4.Api.DataTransfer
{
    public class Issue
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string ApplicationId { get; set; }
        public string VersionId { get; set; }
        public string IssueTypeId { get; set; }
        public string Enviroment { get; set; }
        public string User { get; set; }
        public DateTime Time { get; set; }
        public IDictionary<string, string> Data { get; set; } 

    }
}