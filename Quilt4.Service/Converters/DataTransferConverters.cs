using System;
using Newtonsoft.Json;
using Quilt4.Service.Entity;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Converters
{
    internal static class DataTransferConverters
    {
        public static ProjectResponse ToProjectData(this ProjectPageProject x)
        {
            return new ProjectResponse
            {
                Name = x.Name,
                DashboardColor = x.DashboardColor,
                ProjectApiKey = x.ClientToken,
                ProjectKey = x.Id
            };
        }

        public static SessionRequest ToSessionRequest(this object request)
        {
            var requestString = JsonConvert.SerializeObject(request);
            var data = JsonConvert.DeserializeObject<SessionRequest>(requestString);
            return data;
        }

        public static IssueRequest ToIssueRequest(this object request)
        {
            var requestString = JsonConvert.SerializeObject(request);
            var data = JsonConvert.DeserializeObject<IssueRequest>(requestString);
            return data;
        }
    }
}