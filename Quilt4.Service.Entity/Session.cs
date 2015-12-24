using System;

namespace Quilt4.Service.Entity
{
    public class Session
    {
        public Guid SessionKey { get; set; }
        public Guid ProjectKey { get; set; }
        //public string ApplicationName { get; set; } //TODO: What is this needed for?
        public Guid ApplicationKey { get; set; }
        //public string Version { get; set; } //TODO: What is this needed for?
        public Guid VersionKey { get; set; }
        public string CallerIp { get; set; }
    }
}