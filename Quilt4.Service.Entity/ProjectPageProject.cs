using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class ProjectPageProject
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
        public string ProjectApiKey { get; set; }
        public IEnumerable<ProjectPageApplication> Applications { get; set; }
    }
}