using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class IssueBusiness : IIssueBusiness
    {
        private readonly IRepository _repository;
        private readonly IWriteBusiness _writeBusiness;
        private readonly IUserAccessBusiness _userAccessBusiness;
        private readonly Random _random = new Random();
        private static readonly object _syncRoot = new object();

        public IssueBusiness(IRepository repository, IWriteBusiness writeBusiness, IUserAccessBusiness userAccessBusiness)
        {
            _repository = repository;
            _writeBusiness = writeBusiness;
            _userAccessBusiness = userAccessBusiness;
        }

        public RegisterIssueResponseEntity RegisterIssue(RegisterIssueRequestEntity request)
        {
            //TODO: Log the latest incoming data for each project, so that the user can view it.

            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (string.IsNullOrEmpty(request.SessionKey)) throw new ArgumentException("No valid session provided.");
            if (request.IssueType == null) throw new ArgumentException("No IssueType object in request was provided. Need object '{ \"IssueType\":{...} }' in root.");
            if (string.IsNullOrEmpty(request.IssueType.Message)) throw new ArgumentException("No message in issue type provided.");

            var issueKey = request.IssueKey;
            if (issueKey == Guid.Empty)
            {
                issueKey = Guid.NewGuid();
            }

            var value = request.IssueType.Type;
            if (string.IsNullOrEmpty(value))
            {
                value = "Message";
            }

            var serverTime = DateTime.UtcNow;
            var clientTime = request.ClientTime;
            if (request.ClientTime == DateTime.MinValue)
            {
                clientTime = serverTime;
            }

            var session = _repository.GetSession(request.SessionKey);
            if (session == null)
            {
                throw new ArgumentException("There is no session with provided sessionKey.");
            }
            if (session.ServerEndTime != null)
            {
                throw new InvalidOperationException("The session has already been marked as ended. Create a new session to register issues.");
            }

            int ticket;
            lock (_syncRoot)
            {
                _repository.SetSessionUsed(session.SessionKey, serverTime);
                ticket = GetTicket(session.ProjectKey, 10);
            }

            // Add/Update IssueType
            var issueTypeKey = _repository.GetIssueTypeKey(session.VersionKey, value, request.Level, request.IssueType.Message, request.IssueType.StackTrace);
            if (issueTypeKey == null)
            {
                issueTypeKey = Guid.NewGuid();
                _repository.CreateIssueType(issueTypeKey.Value, session.VersionKey, ticket, value, request.Level, request.IssueType.Message, request.IssueType.StackTrace, serverTime);
            }
            //TODO: Check if the issue key already exists. _repository.GetIssue(issueKey);
            _repository.CreateIssue(issueKey, issueTypeKey.Value, session.SessionKey, clientTime, GetData(request), serverTime);

            _writeBusiness.RunRecalculate();

            return new RegisterIssueResponseEntity(issueKey, ticket, serverTime, session.ProjectKey);
        }

        //public IEnumerable<IssueType> GetIssueTypeList(string userName)
        //{
        //    return _repository.GetIssueTypes(userName);
        //}

        private Dictionary<string, string> GetData(RegisterIssueRequestEntity request)
        {
            var values = GetData(request.IssueType).ToArray();

            var result = new Dictionary<string, string>();
            foreach (var value in values)
            {
                var keyCount = values.Count(x => x.Key == value.Key);
                if (keyCount == 1)
                {
                    result.Add(value.Key, value.Value);
                }
                else
                {
                    var index = 0;
                    while (result.ContainsKey(value.Key + "." + index))
                    {
                        index++;
                    }
                    result.Add(value.Key + "." + index, value.Value);
                }
            }

            return result;
        }

        private IEnumerable<KeyValuePair<string,string>> GetData(IssueTypeRequestEntity issueTypeRequestEntity)
        {
            if (issueTypeRequestEntity == null)
                yield break;

            if (issueTypeRequestEntity.Data != null)
            {
                foreach (var data in issueTypeRequestEntity.Data)
                {
                    yield return new KeyValuePair<string, string>(data.Key, data.Value);
                }
            }

            if (issueTypeRequestEntity.Inner != null)
            {
                foreach (var inner in issueTypeRequestEntity.Inner)
                {
                    var response = GetData(inner);
                    foreach (var item in response)
                        yield return item;
                }
            }
        }

        public IEnumerable<IssueType> GetIssueTypeList(string userName, Guid versionKey)
        {
            var result = _repository.GetIssueTypes(versionKey).ToArray();
            _userAccessBusiness.AssureAccess(userName, result.Select(x => x.ProjectKey));
            return result;
        }

        public IEnumerable<RegisterIssueResponseEntity> GetIssueList(string userName, Guid versionKey)
        {
            var result = _repository.GetIssues(versionKey).ToArray();
            _userAccessBusiness.AssureAccess(userName, result.Select(x => x.ProjectKey));
            return result;
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
                    var waitTimeMs = tryCount * _random.Next(1, 100);
                    tryCount--;
                    System.Diagnostics.Debug.WriteLine("Waiting for {1} ms, will continue to wait {0} more times if needed.", tryCount, waitTimeMs);
                    Thread.Sleep(waitTimeMs);
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