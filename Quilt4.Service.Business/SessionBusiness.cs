using System;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class SessionBusiness : ISessionBusiness
    {
        private readonly IRepository _repository;
        private readonly IWriteBusiness _writeBusiness;

        public SessionBusiness(IRepository repository, IWriteBusiness writeBusiness)
        {
            _repository = repository;
            _writeBusiness = writeBusiness;
        }

        public RegisterSessionResponseEntity RegisterSession(RegisterSessionRequestEntity request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (request.Application == null) throw new ArgumentException("No application provided.");
            if (string.IsNullOrEmpty(request.Application.Name)) throw new ArgumentException("No application name provided.");
            if (string.IsNullOrEmpty(request.Application.Version)) throw new ArgumentException("No application version provided.");
            if(request.ClientStartTime == DateTime.MinValue) throw new ArgumentException("No client start time provided.");

            var serverTime = DateTime.UtcNow;
            var projectKey = _repository.GetProjectKey(request.ProjectApiKey);
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
            var sessionToken = RandomUtility.GetRandomString(32); //TODO: Check that this session is really unique.            
            _repository.CreateSession(sessionToken, request.ClientStartTime, request.CallerIp, versionKey.Value, applicationUserKey, machineKey, ValidationHelper.SetIfEmpty(request.Environment, null), serverTime);

            _writeBusiness.RunRecalculate();

            return new RegisterSessionResponseEntity { SessionToken = sessionToken, ServerStartTime = serverTime};
        }

        public void EndSession(string sessionToken, string callerIp)
        {
            var session = _repository.GetSession(sessionToken);
            if (session.CallerIp != callerIp)
            {
                throw new InvalidOperationException("The call for this session comes from a different origin than the initial call.");
            }

            _repository.SetSessionEnd(sessionToken, DateTime.UtcNow);
        }
    }
}