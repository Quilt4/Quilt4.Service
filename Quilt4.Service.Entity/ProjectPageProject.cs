using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class ProjectPageProject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
        public string ClientToken { get; set; }
        public IEnumerable<ProjectPageApplication> Applications { get; set; }
    }

    public class ProjectPageApplication
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Versions { get; set; }
    }
    
}