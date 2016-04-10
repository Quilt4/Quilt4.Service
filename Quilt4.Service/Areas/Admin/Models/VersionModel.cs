using System;

namespace Quilt4.Service.Areas.Admin.Models
{
    public class VersionDetailsModel
    {
        public Guid VersionKey { get; set; }
        public string VersionNumber { get; set; }
        public IssueTypeModel[] IssueTypes { get; set; }
    }

    public class IssueTypeModel
    {
        public Guid IssueTypeKey { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public int IssueCount { get; set; }
        public string Level { get; set; }
        public DateTime FirstIssue { get; set; }
        public DateTime LastIssue { get; set; }
        public string[] Enviroments { get; set; }
        public string Message { get; set; }
    }

    public class VersionModel
    {
        public Guid VersionKey { get; set; }
        public string VersionNumber { get; set; }
    }
}