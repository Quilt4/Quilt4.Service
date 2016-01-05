using System.Reflection;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class ServiceBusiness : IServiceBusiness
    {
        private readonly IRepository _repository;

        public ServiceBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public Entity.ServiceInfo GetServiceInfo()
        {
            var assembly = Assembly.GetAssembly(typeof(ServiceBusiness));

            var databaseInfo = _repository.GetDatabaseInfo();
            return new Entity.ServiceInfo(assembly.GetName().Version.ToString(), "N/A", databaseInfo.CanConnect ? $"Database {databaseInfo.Database}, Patch version {databaseInfo.Version}." : "Cannot connect to database.");
        }
    }
}