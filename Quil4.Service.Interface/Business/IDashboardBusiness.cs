using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IDashboardBusiness
    {
        IEnumerable<DashboardPageProject> GetProjects(string userId);
    }
}