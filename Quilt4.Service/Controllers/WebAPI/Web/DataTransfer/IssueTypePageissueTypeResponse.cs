using System.Collections.Generic;

namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class IssueTypePageIssueTypeResponse
    {
        public string IssueTypeKey { get; set; }
        public string ProjectKey { get; set; }
        public string ApplicationKey { get; set; }
        public string VersionKey { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public string VersionNumber { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public IEnumerable<InnerIssueTypeResponse> InnerIssueType { get; set; }
        public IEnumerable<IssueTypePageIssueResponse> Issues { get; set; }
    }
}