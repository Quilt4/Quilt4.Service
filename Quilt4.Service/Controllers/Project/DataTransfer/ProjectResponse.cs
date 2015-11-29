using System;

namespace Quilt4.Service.Controllers.Project.DataTransfer
{
    public class ProjectResponse
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }
}