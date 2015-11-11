using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Converters.Application
{
    public static class ApplicationConverter
    {
        public static IEnumerable<ApplicationResponse> ToApplicationResponses(this IEnumerable<Entity.Application> items)
        {
            return items?.Select(x => x.ToApplicationResponse());
        }

        public static ApplicationResponse ToApplicationResponse(this Entity.Application item)
        {
            if (item == null)
                return null;

            return new ApplicationResponse
            {
                Id = item.Id.ToString(),
                ProjectId = item.ProjectId.ToString(),
                Name = item.Name,
                Versions = item.Versions,
                IssueTypes = item.IssueTypes,
                Issues = item.Issues,
                Sessions = item.Sessions
            };
        }
    }
    
}