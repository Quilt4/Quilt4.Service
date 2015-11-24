using System;
using System.Threading.Tasks;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class SessionBusiness : ISessionBusiness
    {
        private readonly IRepository _repository;
        private readonly IWriteRepository _writeRepository;

        public SessionBusiness(IRepository repository, IWriteRepository writeRepository)
        {
            _repository = repository;
            _writeRepository = writeRepository;
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

            Task.Factory.StartNew(() => UpdateReadViews(projectId.Value, applicaitonId, versionId));
        }

        private void UpdateReadViews(Guid projectId, Guid applicaitonId, Guid versionId)
        {
            _writeRepository.UpdateDashboardPageProject(projectId);

            _writeRepository.UpdateProjectPageProject(projectId);
            _writeRepository.UpdateProjectPageApplication(projectId, applicaitonId);
            _writeRepository.UpdateProjectPageVersion(projectId, applicaitonId, versionId);

            _writeRepository.UpdateVersionPageVersion(projectId, applicaitonId, versionId);
            //No need to update VersionPageIssueType, nothing has changed at this moment.

            //No need to update IssueTypePageIssueType, nothing has changed at this moment.
            //No need to update IssueTypePageIssue, nothing has changed at this moment.
        }
    }
}