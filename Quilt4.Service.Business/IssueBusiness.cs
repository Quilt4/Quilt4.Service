using System;
using System.Threading.Tasks;
using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Business
{
    public class IssueBusiness : IIssueBusiness
    {
        private readonly IRepository _repository;
        public IssueBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public RegisterIssueResponseEntity RegisterIssue(RegisterIssueRequestEntity request)
        {

            if (request == null) throw new ArgumentNullException("request", "No request object provided.");
            if (request.Session == null) throw new ArgumentException("No session object in request was provided. Need object '{ \"Session\":{...} }' in root.");
            if (request.Session.SessionId == Guid.Empty) throw new ArgumentException("No valid session guid provided.");
            if (string.IsNullOrEmpty(request.Session.ClientToken)) throw new ArgumentException("No ClientToken provided.");
            if (request.IssueType == null) throw new ArgumentException("No IssueType object in request was provided. Need object '{ \"IssueType\":{...} }' in root.");
            if (string.IsNullOrEmpty(request.IssueType.Message)) throw new ArgumentException("No message in issue type provided.");
            if (string.IsNullOrEmpty(request.IssueType.IssueLevel)) throw new ArgumentException("No issue level in issue type provided.");
            if (request.Session.Application == null) throw new ArgumentException("No application provided!");
            if (string.IsNullOrEmpty(request.Session.Application.SupportToolkitNameVersion)) throw new ArgumentException("No SupportToolkitNameVersion provided!");
            if(string.IsNullOrEmpty(request.IssueType.Type)) throw new ArgumentException("No issue type provided!");
            if(request.Session.ClientStartTime == DateTime.MinValue) throw new ArgumentException("No session client time provided!");
            if(request.ClientTime == DateTime.MinValue) throw new ArgumentException("No client time provided!");
            if (request.Session.User == null) throw new ArgumentException("No user data provided!");
            if (string.IsNullOrEmpty(request.Session.Environment)) throw new ArgumentException("No enviroment provided!");
            if (string.IsNullOrEmpty(request.Session.User.Fingerprint)) throw new ArgumentException("No user fingerprint provided!");
            if (string.IsNullOrEmpty(request.Session.User.UserName)) throw new ArgumentException("No username provided!");
            if (request.Session.Machine == null) throw new ArgumentException("No machine data provided!");
            if (string.IsNullOrEmpty(request.Session.Machine.Fingerprint)) throw new ArgumentException("No machine fingerprint provided!");
            if (string.IsNullOrEmpty(request.Session.Machine.Name)) throw new ArgumentException("No machine name provided!");

            var projectId = _repository.GetProjectId(request.Session.ClientToken);
            if (projectId == null)
            {
                throw new ArgumentException("No project with provided clienttoken");
            }

            var ticket = _repository.GetNextTicket(request.Session.ClientToken, request.Session.Application.Name,
                request.Session.Application.Version, request.IssueType.Type, request.IssueType.IssueLevel, request.IssueType.Message, request.IssueType.StackTrace);
            
            Task.Factory.StartNew(() => SaveIssue(request, ticket, projectId.Value));

            return new RegisterIssueResponseEntity
            {
                Ticket = ticket
            };
        }

        private void SaveIssue(RegisterIssueRequestEntity request, int ticket, Guid projectId)
        {

            // Add/Update Application
            var applicaitonId = _repository.SaveApplication(projectId, request.Session.Application.Name);

            // Add/Update Version
            var versionId = _repository.SaveVersion(applicaitonId, request.Session.Application.Version,
                request.Session.Application.SupportToolkitNameVersion);

            // Add/Update IssueType
            var issueTypeId = _repository.SaveIssueType(versionId, ticket, request.IssueType.Type,
                request.IssueType.IssueLevel, request.IssueType.Message,
                request.IssueType.StackTrace);

            // Add/Update IssueType
            var sessionId = _repository.SaveSession(request.Session.SessionId, request.Session.ClientStartTime,
                request.Session.CallerIp);

            // Add/Update UserData
            var userDataId = _repository.SaveUserData(request.Session.User.Fingerprint, request.Session.User.UserName);

            // Add/Update Machine
            var machineId = _repository.SaveMachine(request.Session.Machine.Fingerprint, request.Session.Machine.Name,
                request.Session.Machine.Data);

            var issueId = _repository.SaveIssue(request.Id, issueTypeId, sessionId, userDataId, machineId, request.ClientTime, request.Session.Environment, request.Data);

            //TODO: Generate data into "read tables" in db
        }
    }
}