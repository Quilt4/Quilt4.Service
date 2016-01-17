using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class VersionPageVersionResponse
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string ApplicationId { get; set; }
        public string Version { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public IEnumerable<VersionPageIssueTypeResponse> IssueTypes { get; set; }
    }
}