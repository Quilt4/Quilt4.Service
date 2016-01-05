namespace Quilt4.Service.Entity
{
    public class ServiceInfo
    {
        public ServiceInfo(string version, string environment, string databaseInfo)
        {
            Version = version;
            Environment = environment;
            DatabaseInfo = databaseInfo;
        }

        public string Version { get; }
        public string Environment { get; }
        public string DatabaseInfo { get; }
    }
}