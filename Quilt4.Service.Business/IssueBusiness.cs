using System;
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
            
            var ticket = _repository.GetNextTicket(request.Session.ClientToken, request.Session.Application.Name,
                request.Session.Application.Version, request.IssueType.Type, request.IssueType.IssueLevel, request.IssueType.Message, request.IssueType.StackTrace);

            //TODO: Start new task to persist data (and don't wait for it to return), and return ticket 

            return null;
        }
    }
}