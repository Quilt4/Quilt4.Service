using System.Linq;
using Quilt4.Service.Areas.Admin.Models;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Areas.Admin.Controllers.Converters
{
    internal static class Converter
    {
        public static ProjectModel ToProjectModel(this ProjectPageProject x)
        {
            return new ProjectModel { ProjectKey = x.ProjectKey, Name = x.Name, ApplicationCount = x.Applications.Count() };
        }
    }
}