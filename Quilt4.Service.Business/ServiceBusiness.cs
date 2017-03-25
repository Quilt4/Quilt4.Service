//using System;
//using Quilt4.Service.Entity;
//using Quilt4.Service.Interface.Business;
//using Quilt4.Service.Interface.Repository;
//using Quilt4Net.Core.Interfaces;

//namespace Quilt4.Service.Business
//{
//    public class ServiceBusiness : IServiceBusiness
//    {
//        private static DatabaseInfo _databaseInfo;

//        private readonly IRepository _repository;
//        private readonly ISourceRepository _sourceRepository;
//        private readonly IConfigurationRepository _configurationRepository;
//        private readonly IServiceLog _serviceLog;
//        private readonly ISettingBusiness _settingBusiness;
//        private readonly ISessionHandler _sessionHandler;
//        private readonly IProjectRepository _projectRepository;

//        public ServiceBusiness(IRepository repository, ISourceRepository sourceRepository, IConfigurationRepository configurationRepository, IServiceLog serviceLog, ISettingBusiness settingBusiness, Quilt4Net.Interfaces.ISessionHandler sessionHandler, IProjectRepository projectRepository)
//        {
//            _repository = repository;
//            _sourceRepository = sourceRepository;
//            _configurationRepository = configurationRepository;
//            _serviceLog = serviceLog;
//            _settingBusiness = settingBusiness;
//            _sessionHandler = sessionHandler;
//            _projectRepository = projectRepository;
//        }

//        public DatabaseInfo GetDatabaseInfo()
//        {
//            return _databaseInfo ?? (_databaseInfo = _configurationRepository.GetDatabaseInfo());
//        }

//        public ServiceInfo GetServiceInfo()
//        {
//            var databaseInfo = GetDatabaseInfo();

//            var version = _sessionHandler.Client.Information.Application.GetApplicationData().Version;
//            var environment = _sessionHandler.Environment;

//            Exception exception;
//            var canWriteToLog = _serviceLog.CanWriteToLog(out exception);
//            var hasOwnProjectApiKey = databaseInfo.CanConnect && _settingBusiness.HasSetting(ConstantSettingKey.ProjectApiKey, true);

//            return new ServiceInfo(version, environment, _sessionHandler.ClientStartTime, databaseInfo, canWriteToLog, hasOwnProjectApiKey);
//        }

//        public void LogApiCall(Guid callKey, string sessionKey, string projectApiKey, DateTime time, TimeSpan elapsed, string callerIp, string currentUserName, string requestType, string path, string request, string response, Guid? issueKey)
//        {
//            Guid? projectKey = null;
//            if (!string.IsNullOrEmpty(projectApiKey))
//            {
//                projectKey = _projectRepository.GetProjectKey(projectApiKey);
//                if (projectKey == null && !string.IsNullOrEmpty(sessionKey))
//                {
//                    projectKey = _repository.GetSession(sessionKey).ProjectKey;
//                }
//            }

//            //TODO: Log two ways.
//            //For the project...
//            _sourceRepository.LogApiCall(callKey, sessionKey, projectKey, time, elapsed, callerIp, currentUserName, requestType, path, request, response, issueKey);

//            //TODO: Send to optional alternative external logers here. (Theese loggers should be registered when starting the project)
//            //_sourceRepository.LogCommand(callKey, time, request);

//            //For the source to be replayed (commands only)
//        }
//    }
//}