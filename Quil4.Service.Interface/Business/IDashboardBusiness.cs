using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IDashboardBusiness
    {
        IEnumerable<IDashboardPageProject> GetProjects(string userId);
    }
}