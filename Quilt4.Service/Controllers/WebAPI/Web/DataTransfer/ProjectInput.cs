using System;

namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class ProjectInput
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }
}