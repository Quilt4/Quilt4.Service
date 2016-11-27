using System;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IServiceBusiness
    {
        DatabaseInfo GetDatabaseInfo();
        ServiceInfo GetServiceInfo();
        void LogApiCall(Guid callKey, string sessionKey, string projectApiKey, DateTime time, TimeSpan elapsed, string callerIp, string currentUserName, string requestType, string callPath, string request, string response, Guid? issueKey);
    }
}