using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IServiceBusiness
    {
        DatabaseInfo GetDatabaseInfo();
        ServiceInfo GetServiceInfo();
    }
}