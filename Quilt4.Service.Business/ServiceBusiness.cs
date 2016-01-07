using System;
using System.Reflection;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;
using Quilt4Net.Core.Interfaces;

namespace Quilt4.Service.Business
{
    public class ServiceBusiness : IServiceBusiness
    {
        private readonly IRepository _repository;
        private readonly IServiceLog _serviceLog;
        private readonly ISettingBusiness _settingBusiness;
        private readonly IQuilt4NetClient _quilt4NetClient;

        public ServiceBusiness(IRepository repository, IServiceLog serviceLog, ISettingBusiness settingBusiness, IQuilt4NetClient quilt4NetClient)
        {
            _repository = repository;
            _serviceLog = serviceLog;
            _settingBusiness = settingBusiness;
            _quilt4NetClient = quilt4NetClient;
        }

        public Entity.ServiceInfo GetServiceInfo()
        {
            var databaseInfo = _repository.GetDatabaseInfo();

            var version = _quilt4NetClient.Information.Aplication.Version;
            var environment = _quilt4NetClient.Session.Environment;

            Exception exception;
            var canWriteToLog = _serviceLog.CanWriteToLog(out exception);            
            var hasOwnProjectApiKey = databaseInfo.CanConnect && _settingBusiness.HasSetting("ProjectApiKey");

            return new Entity.ServiceInfo(version, environment, _quilt4NetClient.Session.ClientStartTime, databaseInfo, canWriteToLog, hasOwnProjectApiKey);
        }
    }
}