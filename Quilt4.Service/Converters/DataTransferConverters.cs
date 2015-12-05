using Newtonsoft.Json;
using Quilt4.Service.Entity;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Converters
{
    internal static class DataTransferConverters
    {
        public static ProjectData ToProjectData(this ProjectPageProject x)
        {
            return new ProjectData
            {
                Name = x.Name,
                DashboardColor = x.DashboardColor,
                ProjectApiKey = x.ClientToken,
                ProjectKey = x.Id
            };
        }

        public static SessionData ToSessionData(this object request)
        {
            var requestString = JsonConvert.SerializeObject(request);
            var data = JsonConvert.DeserializeObject<SessionData>(requestString);
            return data;
        }
    }
}