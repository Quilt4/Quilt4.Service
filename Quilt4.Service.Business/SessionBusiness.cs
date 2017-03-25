using System;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class SessionBusiness : ISessionBusiness
    {
        private readonly IRepository _repository;
        private readonly IProjectRepository _projectRepository;
        //private readonly IWriteBusiness _writeBusiness;

        public SessionBusiness(IRepository repository, IProjectRepository projectRepository) //, IWriteBusiness writeBusiness)
        {
            _repository = repository;
            _projectRepository = projectRepository;
            //_writeBusiness = writeBusiness;
        }

        public RegisterSessionResponseEntity RegisterSession(RegisterSessionRequestEntity request)
        {
            //TODO: Log the latest incoming data for each project, so that the user can view it.

            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (request.Application == null) throw new ArgumentException("No application provided.");
            if (string.IsNullOrEmpty(request.Application.Name)) throw new ArgumentException("No application name provided.");
            if (string.IsNullOrEmpty(request.Application.Version)) throw new ArgumentException("No application version provided.");

            var serverTime = DateTime.UtcNow;
            var clientTime = serverTime;
            if (request.ClientStartTime != DateTime.MinValue)
            {
                clientTime = request.ClientStartTime;
            }

            var projectKey = _projectRepository.GetProjectKey(request.ProjectApiKey);
            if (projectKey == null)
            {
                throw new ArgumentException("There is no project with provided projectApiKey.");
            }

            // Add/Update Application
            var applicationKey = _repository.GetApplicationKey(projectKey.Value, request.Application.Name);
            if (applicationKey == null)
            {
                applicationKey = Guid.NewGuid();
                _repository.SaveApplication(applicationKey.Value, projectKey.Value, request.Application.Name, serverTime);
            }

            // Add/Update Version            
            var versionKey = _repository.GetVersionKey(applicationKey.Value, request.Application.Version, request.Application.BuildTime);
            if (versionKey == null)
            {
                versionKey = Guid.NewGuid();
                _repository.SaveVersion(versionKey.Value, applicationKey.Value, request.Application.Version, request.Application.BuildTime, ValidationHelper.SetIfEmpty(request.Application.SupportToolkitNameVersion, "Unknown"), serverTime);
            }

            // Add/Update UserData
            Guid? applicationUserKey = null;
            if (request.User != null && !string.IsNullOrEmpty(request.User.Fingerprint))
            {
                applicationUserKey = _repository.GetApplicationUser(projectKey.Value, request.User.Fingerprint);
                if (applicationUserKey == null)
                {
                    applicationUserKey = Guid.NewGuid();
                    _repository.SaveApplicationUser(applicationUserKey.Value, projectKey.Value, request.User.Fingerprint, request.User.UserName, serverTime);
                }
            }

            // Add/Update Machine
            Guid? machineKey = null;
            if (request.Machine != null && !string.IsNullOrEmpty(request.Machine.Fingerprint))
            {
                machineKey = _repository.GetMachineKey(projectKey.Value, request.Machine.Fingerprint);
                if (machineKey == null)
                {
                    machineKey = Guid.NewGuid();
                    _repository.SaveMachine(machineKey.Value, projectKey.Value, request.Machine.Fingerprint, request.Machine.Name, request.Machine.Data, serverTime);
                }
            }

            // Add/Update Session
            var sessionKey = RandomUtility.GetRandomString(32); //TODO: Check that this session is really unique.
            _repository.CreateSession(sessionKey, clientTime, request.CallerIp, versionKey.Value, applicationUserKey, machineKey, ValidationHelper.SetIfEmpty(request.Environment, null), serverTime);

            //_writeBusiness.RunRecalculate();

            return new RegisterSessionResponseEntity { SessionKey = sessionKey, ServerStartTime = serverTime};
        }

        public void EndSession(string sessionKey, string callerIp)
        {
            var session = _repository.GetSession(sessionKey);
            if (session.CallerIp != callerIp)
            {
                throw new InvalidOperationException("The call for this session comes from a different origin than the initial call.");
            }

            _repository.SetSessionEnd(sessionKey, DateTime.UtcNow);
        }
    }
}