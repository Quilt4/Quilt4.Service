using System;

namespace Quilt4.Service.Entity
{
    public class Application
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public int Versions { get; set; }
        public int Sessions { get; set; }
        public int IssueTypes { get; set; }
        public int Issues { get; set; }
    }
}