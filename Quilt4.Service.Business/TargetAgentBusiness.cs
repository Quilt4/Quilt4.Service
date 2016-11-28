using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfluxDB.Net;
using InfluxDB.Net.Enums;
using InfluxDB.Net.Models;
using Newtonsoft.Json.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class InfluxDbTargetAgent : ITargetAgent
    {
        private readonly Action<Guid, Exception> _logException;
        private readonly InfluxDb _client;
        private readonly string _database;

        public InfluxDbTargetAgent(string connection, Action<Guid, Exception> logException)
        {
            _logException = logException;
            IDictionary<string, JToken> jsonObject = JObject.Parse(connection);

            var url = jsonObject["url"].Value<string>();
            var username = jsonObject["username"].Value<string>();
            var password = jsonObject["password"].Value<string>();
            _database = jsonObject["database"].Value<string>();

            _client = new InfluxDb(url, username, password, InfluxVersion.Auto);
        }

        public async Task RegisterIssueAsync(RegisterIssueRequestEntity request, RegisterIssueResponseEntity response, Dictionary<string,object> metadata)
        {
            try
            {
                var tags = new Dictionary<string, object>
                {
                    { "SessionKey", request.SessionKey },
                    { "IssueKey", request.IssueKey },
                    { "IssueLevel", request.IssueLevel },
                    { "Ticket", response.Ticket },
                    { "ProjectKey", response.ProjectKey },
                    { "IssueType.Message", request.IssueType.Message },
                    { "IssueType.Type", request.IssueType.Type },
                };

                if (request.IssueThreadKey != null)
                    tags.Add("IssueThreadKey", request.IssueThreadKey);

                if (request.UserHandle != null)
                    tags.Add("UserHandle", request.UserHandle);

                if (request.UserHandle != null)
                    tags.Add("IssueType.StackTrace", request.IssueType.StackTrace);

                if (request.IssueType.Data != null)
                {
                    foreach (var data in request.IssueType.Data)
                    {
                        tags.Add(data.Key, data.Value);
                    }
                }

                foreach (var data in metadata.Where(x => x.Value != null))
                {
                    tags.Add(data.Key, data.Value);
                }

                var fields = new Dictionary<string, object>
                {
                    { "Count", 1 }
                };

                var points = new[]
                {
                    new Point
                    {
                        Measurement = "Issue",
                        Fields = fields,
                        Precision = TimeUnit.Milliseconds,
                        Tags = tags,
                        Timestamp = response.ServerTime
                    }
                };
                await _client.WriteAsync(_database, points);
            }
            catch (Exception exception)
            {
                _logException(response.ProjectKey, exception);
            }
        }
    }

    public class TargetAgentBusiness : ITargetAgentBusiness, IDisposable
    {
        private readonly IRepository _repository;

        public TargetAgentBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
        }

        public IEnumerable<ITargetAgent> GetTargetAgents(Guid projectKey)
        {
            var projectTargets = _repository.GetProjectTargets(projectKey).Where(x => x.Enabled).ToArray();
            foreach (var projectTarget in projectTargets)
            {
                switch (projectTarget.TargetType)
                {
                    case "InfluxDb":
                        yield return new InfluxDbTargetAgent(projectTarget.Connection, LogException);
                        break;
                    case "Kafka":
                    case "Splunk":
                    case "LogStash":
                    case "Custom":
                    case "Quilt4":
                        throw new NotImplementedException();
                    default:
                        throw new ArgumentOutOfRangeException($"Unknown target type {projectTarget.TargetType}.");
                }
            }
        }

        private void LogException(Guid projectKey, Exception exception)
        {
            //TODO: Log issue on the project
        }
    }
}