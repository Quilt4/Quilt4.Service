using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Quil4.Service.Interface.Repository;
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
            if (request == null) throw new ArgumentNullException("request", "No request object provided.");
            if (request.SessionId == Guid.Empty) throw new ArgumentException("No valid session guid provided.");
            if (string.IsNullOrEmpty(request.ClientToken)) throw new ArgumentException("No ClientToken provided.");
            if (request.IssueType == null) throw new ArgumentException("No IssueType object in request was provided. Need object '{ \"IssueType\":{...} }' in root.");
            if (string.IsNullOrEmpty(request.IssueType.Message)) throw new ArgumentException("No message in issue type provided.");
            if (string.IsNullOrEmpty(request.IssueType.IssueLevel)) throw new ArgumentException("No issue level in issue type provided.");
            if (string.IsNullOrEmpty(request.IssueType.Type)) throw new ArgumentException("No issue type provided!");
            if (request.ClientTime == DateTime.MinValue) throw new ArgumentException("No client time provided!");

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

            var ticket = GetTicket(request, 10, session.VersionId);
            
            // Add/Update IssueType
            var issueTypeId = _repository.SaveIssueType(session.VersionId, ticket, request.IssueType.Type,
                request.IssueType.IssueLevel, request.IssueType.Message,
                request.IssueType.StackTrace);

            _repository.SaveIssue(request.Id, issueTypeId, session.Id, request.ClientTime, request.Data);

            WriteBusiness.RunRecalculate();

            return new RegisterIssueResponseEntity
            {
                Ticket = ticket
            };
        }

        private int GetTicket(RegisterIssueRequestEntity request, int tryCount, Guid versionId)
        {
            int ticket;
            try
            {
                ticket = _repository.GetNextTicket(request.ClientToken, request.IssueType.Type, request.IssueType.Message, request.IssueType.StackTrace, request.IssueType.IssueLevel, versionId);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                if (tryCount > 0)
                {
                    Thread.Sleep((10 - tryCount) * 10);
                    tryCount--;
                    ticket = GetTicket(request, tryCount, versionId);
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