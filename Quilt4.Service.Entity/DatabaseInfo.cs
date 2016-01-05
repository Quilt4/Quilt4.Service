namespace Quilt4.Service.Entity
{
    public class DatabaseInfo
    {
        public DatabaseInfo(bool canConnect, string dataSource, string database, int version)
        {
            CanConnect = canConnect;
            DataSource = dataSource;
            Database = database;
            Version = version;
        }

        public string DataSource { get; }
        public string Database { get; }
        public int Version { get; }
        public bool CanConnect { get; }
    }
}