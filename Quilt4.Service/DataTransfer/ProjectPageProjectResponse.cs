using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class ProjectPageProjectResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
        public IEnumerable<ProjectPageApplicationResponse> Applications { get; set; }
    }

    public class ProjectPageApplicationResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Versions { get; set; }
    }
}