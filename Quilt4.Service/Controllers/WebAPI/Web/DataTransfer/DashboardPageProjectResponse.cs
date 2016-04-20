namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class DashboardPageProjectResponse
    {
        public string ProjectKey { get; set; }
        public string Name { get; set; }
        public int VersionCount { get; set; }
        public int SessionCount { get; set; }
        public int IssueTypeCount { get; set; }
        public int IssueCount { get; set; }
        public string DashboardColor { get; set; }
    }
}