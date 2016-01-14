using System;
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
        private readonly ISessionHandler _sessionHandler;

        public ServiceBusiness(IRepository repository, IServiceLog serviceLog, ISettingBusiness settingBusiness, ISessionHandler sessionHandler)
        {
            _repository = repository;
            _serviceLog = serviceLog;
            _settingBusiness = settingBusiness;
            _sessionHandler = sessionHandler;
        }

        public Entity.ServiceInfo GetServiceInfo()
        {
            var databaseInfo = _repository.GetDatabaseInfo();

            var version = _sessionHandler.Client.Information.Aplication.GetApplicationData().Version;
            var environment = _sessionHandler.Environment;

            Exception exception;
            var canWriteToLog = _serviceLog.CanWriteToLog(out exception);            
            var hasOwnProjectApiKey = databaseInfo.CanConnect && _settingBusiness.HasSetting(ConstantSettingKey.ProjectApiKey, true);

            return new Entity.ServiceInfo(version, environment, _sessionHandler.ClientStartTime, databaseInfo, canWriteToLog, hasOwnProjectApiKey);
        }
    }
}