using System;

namespace Quilt4.Service.Entity
{
    public class Session
    {
        public Guid Id { get; set; }
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public Guid VersionId { get; set; }
    }
}