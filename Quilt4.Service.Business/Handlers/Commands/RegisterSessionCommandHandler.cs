using System;
using System.Threading.Tasks;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Commands
{
    public class RegisterSessionCommandHandler : CommandHandlerBase<IRegisterSessionCommandInput>
    {
        public RegisterSessionCommandHandler(IDataRepository repository, IUpdateReadRepository writeRepository)
            : base(repository, writeRepository)
        {
        }

        protected override void DoHandle(IRegisterSessionCommandInput request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (request.SessionKey == Guid.Empty) throw new ArgumentException("No valid session key provided.");
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
            if (request.ClientStartTime == DateTime.MinValue) throw new ArgumentException("No client start time provided.");
            if (string.IsNullOrEmpty(request.Environment)) throw new ArgumentException("No enviroment provided.");

            var projectKey = Repository.GetProjectKeyByProjectApiKey(request.ProjectApiKey);
            if (projectKey == null)
            {
                throw new ArgumentException("There is no project with provided ProjectApiKey.");
            }

            // Add/Update Application
            var applicationKey = Guid.NewGuid();
            Repository.SaveApplication(applicationKey, projectKey.Value, request.Application.Name);

            // Add/Update Version
            var versionKey = Guid.NewGuid();
            Repository.SaveVersion(versionKey, applicationKey, request.Application.Version, request.Application.SupportToolkitNameVersion);

            // Add/Update UserData
            var userDataKey = Guid.NewGuid();
            Repository.SaveUserData(userDataKey, request.User.Fingerprint, request.User.UserName);

            // Add/Update Machine
            var machineKey = Guid.NewGuid();
            Repository.SaveMachine(machineKey, request.Machine.Fingerprint, request.Machine.Name, request.Machine.Data);

            // Add/Update Session
            Repository.SaveSession(request.SessionKey, request.ClientStartTime, request.CallerIp, applicationKey, versionKey, userDataKey, machineKey, request.Environment);

            Task.Run(() => UpdateReadViews(projectKey.Value, applicationKey, versionKey));
        }

        private void UpdateReadViews(Guid projectId, Guid applicaitonId, Guid versionId)
        {
            WriteRepository.UpdateDashboardPageProject(projectId);

            WriteRepository.UpdateProjectPageProject(projectId);
            WriteRepository.UpdateProjectPageApplication(projectId, applicaitonId);
            WriteRepository.UpdateProjectPageVersion(projectId, applicaitonId, versionId);

            WriteRepository.UpdateVersionPageVersion(projectId, applicaitonId, versionId);
            //No need to update VersionPageIssueType, nothing has changed at this moment.

            //No need to update IssueTypePageIssueType, nothing has changed at this moment.
            //No need to update IssueTypePageIssue, nothing has changed at this moment.
        }
    }
}