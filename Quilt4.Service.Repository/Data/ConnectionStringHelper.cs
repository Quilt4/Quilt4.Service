using System.Configuration;

namespace Quilt4.Service.SqlRepository.Data
{
    internal class ConnectionStringHelper
    {
        public static string GetConnectionString(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return connectionString;
        }
    }
}