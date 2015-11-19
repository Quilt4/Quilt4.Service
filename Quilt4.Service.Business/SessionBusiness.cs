using System;
using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Business
{
    public class SessionBusiness : ISessionBusiness
    {
        private readonly IRepository _repository;

        public SessionBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public void RegisterSession(SessionRequestEntity request)
        {
            if (request == null) throw new ArgumentNullException("request", "No request object provided.");
            if (request.SessionId == Guid.Empty) throw new ArgumentException("No valid session guid provided.");
            if (request.Application == null) throw new ArgumentException("No application provided.");
            if (string.IsNullOrEmpty(request.Application.Name)) throw new ArgumentException("No application name provided.");
            if (string.IsNullOrEmpty(request.Application.Version)) throw new ArgumentException("No application version provided.");
            if (string.IsNullOrEmpty(request.Application.SupportToolkitNameVersion)) throw new ArgumentException("No application SupportToolkitNameVersion provided.");
            if (request.User == null) throw new ArgumentException("No user provided.");
            if (string.IsNullOrEmpty(request.User.Fingerprint)) throw new ArgumentException("No user fingerprint provided.");
            if (string.IsNullOrEmpty(request.User.UserName)) throw new ArgumentException("No username provided.");
            if (request.Machine == null) throw new ArgumentException("No machine provided.");
            if (string.IsNullOrEmpty(request.Machine.Fingerprint)) throw new ArgumentException("No machine fingerprint provided.");
            if (string.IsNullOrEmpty(request.Machine.Name)) throw new ArgumentException("No machine name provided.");
            if(request.ClientStartTime == DateTime.MinValue) throw new ArgumentException("No client start time provided.");
            if (string.IsNullOrEmpty(request.Environment)) throw new ArgumentException("No enviroment provided.");
            
            var projectId = _repository.GetProjectId(request.ClientToken);
            if (projectId == null)
            {
                throw new ArgumentException("No project with provided clienttoken");
            }

            // Add/Update Application
            var applicaitonId = _repository.SaveApplication(projectId.Value, request.Application.Name);

            // Add/Update Version
            var versionId = _repository.SaveVersion(applicaitonId, request.Application.Version,
                request.Application.SupportToolkitNameVersion);

            // Add/Update UserData
            var userDataId = _repository.SaveUserData(request.User.Fingerprint, request.User.UserName);

            // Add/Update Machine
            var machineId = _repository.SaveMachine(request.Machine.Fingerprint, request.Machine.Name,
                request.Machine.Data);

            // Add/Update Session
            _repository.SaveSession(request.SessionId, request.ClientStartTime,
                request.CallerIp, applicaitonId, versionId, userDataId, machineId, request.Environment);
        }
    }
}