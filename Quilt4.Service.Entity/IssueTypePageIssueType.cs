﻿using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class IssueTypePageIssueType
    {
        public Guid IssueTypeKey { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid VersionId { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public IEnumerable<IssueTypePageIssue> Issues { get; set; }
    }
}