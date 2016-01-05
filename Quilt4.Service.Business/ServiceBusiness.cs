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
            var quilt4NetClient = new Quilt4Net.Quilt4NetClient(new Quilt4Net.Configuration());
            var version = quilt4NetClient.Information.Aplication.Version;
            var environment = quilt4NetClient.Session.Environment;

            var databaseInfo = _repository.GetDatabaseInfo();
            return new Entity.ServiceInfo(version, environment, quilt4NetClient.Session.ClientStartTime, databaseInfo.CanConnect ? $"Database {databaseInfo.Database}, Patch version {databaseInfo.Version}." : "Cannot connect to database.");
        }
    }
}