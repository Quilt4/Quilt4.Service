using System;

namespace Quilt4.Service.Entity
{
    public class ServiceInfo
    {
        public ServiceInfo(string version, string environment, DateTime startTime, DatabaseInfo databaseInfo, bool canWriteToSystemLog, bool hasOwnProjectApiKey)
        {
            Version = version;
            Environment = environment;
            StartTime = startTime;
            DatabaseInfo = databaseInfo;
            CanWriteToSystemLog = canWriteToSystemLog;
            HasOwnProjectApiKey = hasOwnProjectApiKey;
        }

        public string Version { get; }
        public string Environment { get; }
        public DateTime StartTime { get; }
        public DatabaseInfo DatabaseInfo { get; }
        public bool CanWriteToSystemLog { get; }
        public bool HasOwnProjectApiKey { get; }
    }
}