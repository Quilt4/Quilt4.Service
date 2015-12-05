using Quilt4.Service.Entity;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Controllers.Client.Converters
{
    internal static class ProjectConverter
    {
        public static ProjectResponse ToProjectResponse(this ProjectPageProject x)
        {
            return new Tharga.Quilt4Net.DataTransfer.ProjectResponse
            {
                Name = x.Name,
                DashboardColor = x.DashboardColor,
                ProjectApiKey = x.ClientToken,
                ProjectKey = x.Id
            };
        }
    }
}
