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
        private readonly IWriteRepository _writeRepository;

        public IssueBusiness(IRepository repository, IWriteRepository writeRepository)
        {
            _repository = repository;
            _writeRepository = writeRepository;
        }

        public RegisterIssueResponseEntity RegisterIssue(RegisterIssueRequestEntity request)
        {

            if (request == null) throw new ArgumentNullException("request", "No request object provided.");
            if (request.SessionId == Guid.Empty) throw new ArgumentException("No valid session guid provided.");
            if (string.IsNullOrEmpty(request.ClientToken)) throw new ArgumentException("No ClientToken provided.");
            if (request.IssueType == null) throw new ArgumentException("No IssueType object in request was provided. Need object '{ \"IssueType\":{...} }' in root.");
            if (string.IsNullOrEmpty(request.IssueType.Message)) throw new ArgumentException("No message in issue type provided.");
            if (string.IsNullOrEmpty(request.IssueType.IssueLevel)) throw new ArgumentException("No issue level in issue type provided.");
            if(string.IsNullOrEmpty(request.IssueType.Type)) throw new ArgumentException("No issue type provided!");
            if(request.ClientTime == DateTime.MinValue) throw new ArgumentException("No client time provided!");

            var session = _repository.GetSession(request.SessionId);
            if (session == null)
            {
                throw new ArgumentException("No session with provided sessionId");
            }

            var projectId = _repository.GetProjectId(request.ClientToken);
            if (projectId == null)
            {
                throw new ArgumentException("No project with provided clienttoken");
            }

            var ticket = _repository.GetNextTicket(request.ClientToken, session.ApplicationName,
                session.Version, request.IssueType.Type, request.IssueType.IssueLevel, request.IssueType.Message, request.IssueType.StackTrace);
            
            Task.Factory.StartNew(() => SaveIssue(request, ticket, projectId.Value, session));

            return new RegisterIssueResponseEntity
            {
                Ticket = ticket
            };
        }

        private void SaveIssue(RegisterIssueRequestEntity request, int ticket, Guid projectId, Session session)
        {


            // Add/Update IssueType
            var issueTypeId = _repository.SaveIssueType(session.VersionId, ticket, request.IssueType.Type,
                request.IssueType.IssueLevel, request.IssueType.Message,
                request.IssueType.StackTrace);

            var issueId = _repository.SaveIssue(request.Id, issueTypeId, session.Id, request.ClientTime, request.Data);

            _writeRepository.UpdateDashboardPageProject(projectId);

            _writeRepository.UpdateProjectPageProject(projectId);
            _writeRepository.UpdateProjectPageApplication(projectId, session.ApplicationId);
            _writeRepository.UpdateProjectPageVersion(projectId, session.ApplicationId, session.VersionId);

            _writeRepository.UpdateVersionPageVersion(projectId, session.ApplicationId, session.VersionId);
            _writeRepository.UpdateVersionPageIssueType(projectId, session.ApplicationId, session.VersionId, issueTypeId);

            _writeRepository.UpdateIssueTypePageIssueType(projectId, session.ApplicationId, session.VersionId,
                issueTypeId);
            _writeRepository.UpdateIssueTypePageIssue(projectId, session.ApplicationId, session.VersionId, issueTypeId,
                issueId);
            //IssueTypePageIssue
        }
    }
}