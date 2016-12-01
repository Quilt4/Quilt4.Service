using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfluxDB.Net;
using InfluxDB.Net.Enums;
using InfluxDB.Net.Models;
using Newtonsoft.Json.Linq;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class InfluxDbTargetAgent : ITargetAgent
    {
        private readonly InfluxDb _client;
        private readonly string _database;

        public InfluxDbTargetAgent(string connection)
        {
            IDictionary<string, JToken> jsonObject = JObject.Parse(connection);

            var url = jsonObject["url"].Value<string>();
            var username = jsonObject["username"].Value<string>();
            var password = jsonObject["password"].Value<string>();
            _database = jsonObject["database"].Value<string>();

            _client = new InfluxDb(url, username, password, InfluxVersion.Auto);
        }

        public async Task<bool> RegisterIssueAsync(Guid projectKey, string eventName, DateTime serverTime, Dictionary<string, object> tags, Dictionary<string, object> fields)
        {
            var points = new[]
            {
                new Point
                {
                    Measurement = eventName,
                    Fields = fields.Where(x => x.Value == null).ToDictionary(x => x.Key, x => x.Value),
                    Precision = TimeUnit.Milliseconds,
                    Tags = tags.Where(x => x.Value == null).ToDictionary(x => x.Key, x => x.Value),
                    Timestamp = serverTime
                }
            };
            var response = await _client.WriteAsync(_database, points);

            return response.Success;
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
                        yield return new InfluxDbTargetAgent(projectTarget.Connection);
                        break;
                    case "Kafka":
                    case "Splunk":
                    case "LogStash":
                    case "Custom":
                    case "Quilt4":
                    case "EMail":
                        throw new NotImplementedException();
                    default:
                        throw new ArgumentOutOfRangeException($"Unknown target type {projectTarget.TargetType}.");
                }
            }
        }
    }
}