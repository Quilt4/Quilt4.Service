using System.Collections.Generic;

namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class ProjectPageProjectResponse
    {
        public string ProjectKey { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
        public string ClientToken { get; set; } 
        public IEnumerable<ProjectPageApplicationResponse> Applications { get; set; }
    }
}