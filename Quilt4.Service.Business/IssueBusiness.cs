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
            if (request.SessionKey.IsValidGuid()) throw new ArgumentException("No valid session guid provided.");
            if (request.IssueType == null) throw new ArgumentException("No IssueType object in request was provided. Need object '{ \"IssueType\":{...} }' in root.");
            if (string.IsNullOrEmpty(request.IssueType.Message)) throw new ArgumentException("No message in issue type provided.");
            if (string.IsNullOrEmpty(request.IssueType.IssueLevel)) throw new ArgumentException("No issue level in issue type provided.");
            if (string.IsNullOrEmpty(request.IssueType.Type)) throw new ArgumentException("No issue type provided.");
            if (request.ClientTime == DateTime.MinValue) throw new ArgumentException("No client time provided.");

            var session = _repository.GetSession(request.SessionKey);
            if (session == null)
            {
                throw new ArgumentException("There is no session with provided sessionKey.");
            }

            //var projectId = _repository.GetProjectKey(request.ProjectApiKey);
            //if (projectId == null)
            //{
            //    throw new ArgumentException("No project with provided clienttoken");
            //}

            var ticket = GetTicket(session.ProjectKey, 10); //, request, session.VersionKey);
            
            // Add/Update IssueType
            var issueTypeKey = _repository.SaveIssueType(session.VersionKey, ticket, request.IssueType.Type, request.IssueType.IssueLevel, request.IssueType.Message, request.IssueType.StackTrace);
            _repository.SaveIssue(request.IssueKey, issueTypeKey, session.SessionKey, request.ClientTime, request.Data);

            WriteBusiness.RunRecalculate();

            return new RegisterIssueResponseEntity
            {
                Ticket = ticket
            };
        }

        private int GetTicket(Guid projectKey, int tryCount) //, RegisterIssueRequestEntity request, Guid versionId)
        {
            int ticket;
            try
            {
                ticket = _repository.GetNextTicket(projectKey); //, request.IssueType.Type, request.IssueType.Message, request.IssueType.StackTrace, request.IssueType.IssueLevel, versionId);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                if (tryCount > 0)
                {
                    Thread.Sleep((10 - tryCount) * 10);
                    tryCount--;
                    ticket = GetTicket(projectKey, tryCount); //, request, versionId);
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