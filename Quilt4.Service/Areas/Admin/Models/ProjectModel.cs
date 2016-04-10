using System;

namespace Quilt4.Service.Areas.Admin.Models
{
    public class ProjectModel
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public int ApplicationCount { get; set; }
    }
}