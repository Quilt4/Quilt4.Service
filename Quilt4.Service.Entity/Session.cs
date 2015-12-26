using System;

namespace Quilt4.Service.Entity
{
    public class Session
    {
        public Guid SessionKey { get; set; }
        public Guid ProjectKey { get; set; }
        public Guid ApplicationKey { get; set; }
        public Guid VersionKey { get; set; }
        public string CallerIp { get; set; }
        public DateTime? ServerEndTime { get; set; }
    }
}