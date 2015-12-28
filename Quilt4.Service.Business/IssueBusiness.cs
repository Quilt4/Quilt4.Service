using System;
using System.Threading;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

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
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (!request.SessionKey.IsValidGuid()) throw new ArgumentException("No valid session guid provided.");
            if (request.IssueType == null) throw new ArgumentException("No IssueType object in request was provided. Need object '{ \"IssueType\":{...} }' in root.");
            if (string.IsNullOrEmpty(request.IssueType.Message)) throw new ArgumentException("No message in issue type provided.");
            if (string.IsNullOrEmpty(request.IssueType.IssueLevel)) throw new ArgumentException("No issue level in issue type provided.");
            if (string.IsNullOrEmpty(request.IssueType.Type)) throw new ArgumentException("No issue type provided.");
            if (request.ClientTime == DateTime.MinValue) throw new ArgumentException("No client time provided.");

            var serverTime = DateTime.UtcNow;

            var session = _repository.GetSession(request.SessionKey);
            if (session == null)
            {
                throw new ArgumentException("There is no session with provided sessionKey.");
            }
            if (session.ServerEndTime != null)
            {
                throw new InvalidOperationException("The session has already been marked as ended. Create a new session to register issues.");
            }
            _repository.SetSessionUsed(session.SessionKey, serverTime);

            var ticket = GetTicket(session.ProjectKey, 10);

            // Add/Update IssueType
            var issueTypeKey = _repository.GetIssueTypeKey(session.VersionKey, request.IssueType.Type, request.IssueType.IssueLevel, request.IssueType.Message, request.IssueType.StackTrace);
            if (issueTypeKey == null)
            {
                issueTypeKey = Guid.NewGuid();
                _repository.CreateIssueType(issueTypeKey.Value, session.VersionKey, ticket, request.IssueType.Type, request.IssueType.IssueLevel, request.IssueType.Message, request.IssueType.StackTrace, serverTime);
            }
            _repository.CreateIssue(request.IssueKey, issueTypeKey.Value, session.SessionKey, request.ClientTime, request.Data, serverTime);

            WriteBusiness.RunRecalculate();

            return new RegisterIssueResponseEntity
            {
                Ticket = ticket
            };
        }

        private int GetTicket(Guid projectKey, int tryCount)
        {
            int ticket;
            try
            {
                ticket = _repository.GetNextTicket(projectKey);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                if (tryCount > 0)
                {
                    Thread.Sleep((10 - tryCount) * 10);
                    tryCount--;
                    ticket = GetTicket(projectKey, tryCount);
                }
                else
                {
                    throw;
                }
            }
            return ticket;
        }
    }
}