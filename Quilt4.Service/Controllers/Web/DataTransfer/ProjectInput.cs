using System;

namespace Quilt4.Service.Controllers.Web
{
    public class ProjectInput
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }
}