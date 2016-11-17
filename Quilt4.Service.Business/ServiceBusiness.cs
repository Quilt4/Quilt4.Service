using System;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;
using Quilt4Net.Core.Interfaces;

namespace Quilt4.Service.Business
{
    public class ServiceBusiness : IServiceBusiness
    {
        private static DatabaseInfo _databaseInfo;

        private readonly IRepository _repository;
        private readonly IServiceLog _serviceLog;
        private readonly ISettingBusiness _settingBusiness;
        private readonly ISessionHandler _sessionHandler;

        public ServiceBusiness(IRepository repository, IServiceLog serviceLog, ISettingBusiness settingBusiness, Quilt4Net.Interfaces.ISessionHandler sessionHandler)
        {
            _repository = repository;
            _serviceLog = serviceLog;
            _settingBusiness = settingBusiness;
            _sessionHandler = sessionHandler;
        }

        public DatabaseInfo GetDatabaseInfo()
        {
            return _databaseInfo ?? (_databaseInfo = _repository.GetDatabaseInfo());
        }

        public ServiceInfo GetServiceInfo()
        {
            var databaseInfo = GetDatabaseInfo();

            var version = _sessionHandler.Client.Information.Application.GetApplicationData().Version;
            var environment = _sessionHandler.Environment;

            Exception exception;
            var canWriteToLog = _serviceLog.CanWriteToLog(out exception);
            var hasOwnProjectApiKey = databaseInfo.CanConnect && _settingBusiness.HasSetting(ConstantSettingKey.ProjectApiKey, true);

            return new ServiceInfo(version, environment, _sessionHandler.ClientStartTime, databaseInfo, canWriteToLog, hasOwnProjectApiKey);
        }
    }
}