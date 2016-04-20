using System;

namespace Quilt4.Service.Entity
{
    public class ProjectPageApplication
    {
        public Guid ApplicationKey { get; set; }
        public string Name { get; set; }
        public int VersionCount { get; set; }
    }
}