using System.Collections.Generic;

namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class VersionPageVersionResponse
    {
        public string VersionKey { get; set; }
        public string ProjectKey { get; set; }
        public string ApplicationKey { get; set; }
        public string VersionNumber { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public IEnumerable<VersionPageIssueTypeResponse> IssueTypes { get; set; }
        public IEnumerable<VersionPageSessionResponse> Sessions { get; set; }
    }
}