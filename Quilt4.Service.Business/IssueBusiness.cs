using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;
using Quilt4Net;

namespace Quilt4.Service.Business
{
    public class IssueBusiness : IIssueBusiness
    {
        private readonly IRepository _repository;
        private readonly IReadModelBusiness _readModelBusiness;
        private readonly IServiceLog _serviceLog;
        private readonly IUserAccessBusiness _userAccessBusiness;
        private readonly ITargetAgentBusiness _targetAgentBusiness;
        private readonly Random _random = new Random();
        private static readonly object _syncRoot = new object();

        public IssueBusiness(IRepository repository, IUserAccessBusiness userAccessBusiness, ITargetAgentBusiness targetAgentBusiness, IReadModelBusiness readModelBusiness, IServiceLog serviceLog)
        {
            _repository = repository;
            _userAccessBusiness = userAccessBusiness;
            _targetAgentBusiness = targetAgentBusiness;
            _readModelBusiness = readModelBusiness;
            _serviceLog = serviceLog;
        }

        public RegisterIssueResponseEntity RegisterIssue(RegisterIssueRequestEntity request)
        {
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
            var issueTypeKey = _repository.GetIssueTypeKey(session.VersionKey, value, request.IssueLevel, request.IssueType.Message, request.IssueType.StackTrace);
            if (issueTypeKey == null)
            {
                issueTypeKey = Guid.NewGuid();
                _repository.CreateIssueType(issueTypeKey.Value, session.VersionKey, ticket, value, request.IssueLevel, request.IssueType.Message, request.IssueType.StackTrace, serverTime, request.IssueType.Inner);
            }

            //TODO: Check if the issue key already exists. _repository.GetIssue(issueKey);
            _repository.CreateIssue(issueKey, issueTypeKey.Value, request.IssueThreadKey, session.SessionKey, clientTime, GetData(request), serverTime);

            var response = new RegisterIssueResponseEntity(issueKey, ticket, serverTime, session.ProjectKey, issueTypeKey.Value, session.SessionKey);

            Task.Run(() =>
            {
                try
                {
                    _readModelBusiness.AddIssue(response.IssueKey);
                    SendToSubTargets(request, session, response);
                }
                catch (Exception exception)
                {
                    _serviceLog.LogException(exception, LogLevel.SystemError);
                }
            });

            return response;
        }

        private void SendToSubTargets(RegisterIssueRequestEntity request, Session session, RegisterIssueResponseEntity response)
        {
            var issue = _readModelBusiness.GetIssue(response.IssueKey);
            
            var tags = new Dictionary<string, object>
            {
                { "SessionKey", request.SessionKey },
                { "IssueKey", request.IssueKey },
                { "IssueLevel", request.IssueLevel },
                { "Ticket", response.Ticket },
                { "ProjectKey", response.ProjectKey },
                { "IssueType.Message", request.IssueType.Message },
                { "IssueType.Type", request.IssueType.Type },
                { "ProjectName", issue.ProjectName },
                { "ApplicationName", issue.ApplicationName },
                { "VersionNumber", issue.VersionNumber },
                { "MachineName", issue.MachineName },
                { "IssueThreadKey", request.IssueThreadKey },
                { "UserHandle", request.UserHandle },
                { "IssueType.StackTrace", request.IssueType.StackTrace }
            };

            AppendData(tags, request.IssueType);

            //if (request.IssueType.Data != null)
            //{
            //    //TODO: Aggregate data from all levels of inner exceptions
            //    //request.IssueType.Inner.First().Data

            //    foreach (var data in request.IssueType.Data)
            //    {
            //        tags.Add(data.Key, data.Value);
            //    }
            //}

            var fields = new Dictionary<string, object>
            {
                { "Count", 1 }
            };

            foreach (var targetAgent in _targetAgentBusiness.GetTargetAgents(session.ProjectKey))
            {
                try
                {
                    targetAgent.RegisterIssueAsync(response.ProjectKey, "Issue", response.ServerTime, tags, fields);
                }
                catch (Exception exception)
                {
                    exception.AddData("targetAgent", targetAgent).AddData("ProjectKey", response.ProjectKey);
                    _serviceLog.LogException(exception, LogLevel.SystemError);
                }
            }
        }

        private void AppendData(Dictionary<string, object> tags, IssueTypeRequestEntity requestIssueType)
        {
            foreach (var data in requestIssueType.Data)
            {
                if (tags.ContainsKey(data.Key))
                {
                    var tagValue = tags[data.Key];
                    if (!tagValue.Equals(data.Value))
                    {
                        if (tagValue is List<object>)
                        {
                            var dl = ((List<object>)tagValue);
                            if (!dl.Contains(data.Value))
                            {
                                dl.Add(data.Value);
                            }
                        }
                        else
                        {
                            tags.Remove(data.Key);
                            tags.Add(data.Key, new List<object> { tagValue, data.Value });
                        }
                    }
                }
                else
                {
                    tags.Add(data.Key, data.Value);
                }
            }

            foreach (var inner in requestIssueType.Inner)
            {
                AppendData(tags, inner);
            }
        }

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