using System;

namespace Quilt4.Service.Entity
{
    public class ServiceInfo
    {
        public ServiceInfo(string version, string environment, DateTime startTime, string databaseInfo)
        {
            Version = version;
            Environment = environment;
            StartTime = startTime;
            DatabaseInfo = databaseInfo;
        }

        public string Version { get; }
        public string Environment { get; }
        public DateTime StartTime { get; }
        public string DatabaseInfo { get; }
    }
}