using System;

namespace Quilt4.Service.Entity
{
    public class SessionDetail
    {
        public string SessionKey { get; set; }
        public DateTime StartClientTime { get; set; }
        public DateTime StartServerTime { get; set; }
        public DateTime LastUsedServerTime { get; set; }
        public DateTime? EndServerTime { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public string Environment { get; set; }
        public string CallerIp { get; set; }
    }
}